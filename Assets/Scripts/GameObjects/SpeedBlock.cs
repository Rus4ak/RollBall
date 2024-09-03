using UnityEngine;

public class SpeedBlock : MonoBehaviour
{
    [SerializeField] private float _speed;

    private GameObject _player;
    private Rigidbody _rigidbody;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = _player.GetComponent<Rigidbody>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _player)
        {
            _playerMovement.MaxSpeed = 50f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == _player)
        {
            Vector3 forcePosition = new Vector3();

            forcePosition.x = transform.rotation.eulerAngles.x / 90;
            forcePosition.y = transform.rotation.eulerAngles.z / 540;
            forcePosition.z = transform.rotation.eulerAngles.y / 90;
            
            _rigidbody.velocity = forcePosition * _speed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == _player)
            _playerMovement.MaxSpeed = _playerMovement.Speed;
    }
}
