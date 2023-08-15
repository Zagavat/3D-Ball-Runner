using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private RoadMove _roadScript;
    
    private CharacterController _characterController;
    private float _gravity = 9.81f;
    private bool _isJump = false;
    private bool _isJumping = false;
    private Vector3 _moveDirection;
    private float _roadSpeed;
    private float _forwardRotationSpeed;
    private float _directionAngle;
    private Vector3 _startPosition;
    private float _deathYPosition = -15f;
    private float _criticalYPosition = -3f;

    public event UnityAction GemCollected;
    public event UnityAction ShitHappened;
    public event UnityAction<Vector3> Died;
    public event UnityAction<Vector3> Rolling;
    public event UnityAction<Vector3> Jump;

    private void Start()
    {
        _startPosition = transform.position;
        _characterController = GetComponent<CharacterController>();
        _roadSpeed = _roadScript.GetSpeed();
        _directionAngle = Vector3.Angle(new Vector3(_roadSpeed, 0, 0), new Vector3(_roadSpeed, 0, _slideSpeed));
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        _moveDirection.x = 0;
        _forwardRotationSpeed = -_roadSpeed * Time.deltaTime * 180 / Mathf.PI * 0.5f;
        PollingKeyboard();

        if (_characterController.isGrounded == true)
        {
            Rolling?.Invoke(transform.position);

            if (_isJump == true)
            {
                _isJumping = true;
                _moveDirection.y = _jumpForce;
                Jump?.Invoke(transform.position);
            }

            SlopeSliding();
        }

        _moveDirection.y -= _gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
        transform.Rotate(0, 0, _forwardRotationSpeed);

        if (_characterController.isGrounded == true && _isJumping == true)
        {
            _isJumping = false;
            _isJump = false;
            _moveDirection.y = _startPosition.y;
        }
    }

    private void FixedUpdate()
    {
        if(transform.position.y < _criticalYPosition)
        {
            Died?.Invoke(transform.position);

            if (transform.position.y < _deathYPosition)
            {
                _isJump = false;
                _isJumping = false;
                transform.SetPositionAndRotation(_startPosition, Quaternion.identity);
                _moveDirection = Vector3.zero;
                Die();
            }
        }
    }

    private void Die()
    {
        ShitHappened?.Invoke();
    }

    private void PollingKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveDirection.z = _slideSpeed;
            ChangeDirection(-_directionAngle);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveDirection.z = -_slideSpeed;
            ChangeDirection(_directionAngle);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _moveDirection.z = 0f;
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _isJump = true;
        }
    }

    private void ChangeDirection(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    private void SlopeSliding()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (slopeAngle > _characterController.slopeLimit)
            {
                _moveDirection += Vector3.ProjectOnPlane(hit.normal, Vector3.right) * _slideSpeed * Time.deltaTime * _gravity;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jumper")
        {
            _isJump = true;
        }

        if (other.tag == "Collect")
        {
            if (other.TryGetComponent(out Gem gem))
            {
                gem.StartCollectEffect();
            }

            GemCollected?.Invoke();
        }
    }
}
