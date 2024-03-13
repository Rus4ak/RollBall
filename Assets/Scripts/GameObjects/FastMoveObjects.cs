using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FastMoveObjects : MonoBehaviour
{
    [SerializeField][Range(-1, 1)] private int _directionX;
    [SerializeField][Range(-1, 1)] private int _directionY;
    [SerializeField][Range(-1, 1)] private int _directionZ;
    [SerializeField] private float _force;
    [SerializeField] private Vector3 _endPosition;

    private Vector3 _direction;
    private Rigidbody _rb;
    private bool _isStart = true;

    private void Start()
    {
        _direction = new Vector3(_directionX, _directionY, _directionZ);
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (_isStart)
            {
                _rb.AddForce(_direction * _force);
                _isStart = false;
            }
    }

    private void Update()
    {
        if (IsNegative(_endPosition - transform.position))
            _rb.velocity = Vector3.zero;
    }

    private bool IsNegative(Vector3 vector)
    {
        return vector.x <= 0 && vector.y <= 0 && vector.z <= 0;
    }
}
