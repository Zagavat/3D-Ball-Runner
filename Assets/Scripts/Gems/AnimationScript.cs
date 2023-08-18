using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationAngle;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(_rotationAngle * _rotationSpeed * Time.deltaTime);
    }
}