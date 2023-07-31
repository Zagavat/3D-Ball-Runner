using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    [SerializeField] private Transform _roadTile;

    private float _speed = 20;
    private bool _isPitching = false;
    private int _pitchingPercent = 30;
    private float _pitchingAngle = 45;
    private Quaternion _startValue;
    private Quaternion _endValue;
    private float _pitchingSpeed = 0.5f;

    private void Start()
    {
        _startValue = Quaternion.Euler(-_pitchingAngle, 0, 0);
        _endValue = Quaternion.Euler(_pitchingAngle, 0, 0);
    }

    private void FixedUpdate()
    {
        if (_isPitching)
        {
            transform.rotation = Quaternion.Lerp(_startValue, _endValue, Mathf.PingPong(Time.time * _pitchingSpeed, 1f));
        }

        transform.Translate(-transform.right * _speed * Time.deltaTime);
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetPitching()
    {
        _isPitching = Random.Range(0, 100) < _pitchingPercent;
    }

    public Vector3 GetTileLocalScale()
    {
        return _roadTile.localScale;
    }
}
