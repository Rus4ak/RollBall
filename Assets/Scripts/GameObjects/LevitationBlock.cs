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
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
       
            _startCollisionTime = Time.time;

            // The player's gravity turns off
            _playerRigidbody.useGravity = false;
            // The player's velocity in Y direction increases by a specified force
            _playerRigidbody.velocity += Vector3.up * _force;

            _isStart = true;
        }
    }

    private void Update()
    {
        if (_isStart)
        {
            // After a certain time has elapsed, the player's gravity turns on
            if (Time.time - _startCollisionTime > _time)
            {
                _playerRigidbody.useGravity = true;
                _isStart = false;
            }
        }
    }
}
