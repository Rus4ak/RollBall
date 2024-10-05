using UnityEngine;

public class FullRotate : MonoBehaviour
{
    [SerializeField, Range(-1f, 1f)] private int _directionX;
    [SerializeField, Range(-1f, 1f)] private int _directionY;
    [SerializeField, Range(-1f, 1f)] private int _directionZ;
    [SerializeField] private float _timeRotate;

    private Vector3 _direction;
    private float _rotateSpeed;

    private void Start()
    {
        _direction = new Vector3(_directionX, _directionY, _directionZ);
        _rotateSpeed = 360f / _timeRotate;
    }

    private void FixedUpdate()
    {
        transform.Rotate(_rotateSpeed * Time.deltaTime * _direction);
    }
}
