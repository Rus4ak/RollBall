using UnityEngine;

public class LevitationBlock : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _time;

    private Rigidbody _playerRigidbody;
    private float _startCollisionTime;
    
    private bool _isStart = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
       
            _startCollisionTime = Time.time;

            _playerRigidbody.useGravity = false;
            _playerRigidbody.velocity += Vector3.up * _force;

            _isStart = true;
        }
    }

    private void Update()
    {
        if (_isStart)
        {
            if (Time.time - _startCollisionTime > _time)
            {
                _playerRigidbody.useGravity = true;
                _isStart = false;
            }
        }
    }
}
