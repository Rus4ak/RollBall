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
    private bool _isStart = false;

    private void Start()
    {
        _direction = new Vector3(_directionX, _directionY, _directionZ);
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _isStart = true;
    }

    // The object moves in a given direction with a given force to a given position using AddForce
    private void FixedUpdate()
    {
        if (_isStart)
        {
            _rb.AddForce(_direction * _force);
        }

        if (IsStop(_endPosition - transform.position))
        {
            _isStart = false;
            _rb.velocity = Vector3.zero;
            _rb.constraints = RigidbodyConstraints.FreezePosition;
            _rb.freezeRotation = true;
        }
    }

    private bool IsStop(Vector3 vector)
    {
        return Mathf.Min(vector.x, vector.y, vector.z) >= -1 && Mathf.Max(vector.x, vector.y, vector.z) <= 1;
    }
}
