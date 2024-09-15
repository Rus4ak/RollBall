using UnityEngine;

public class SpeedBlock : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _defaultSpeed;
    private GameObject _player;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _player)
        {
            _defaultSpeed = _playerMovement.Speed;
            _playerMovement.Speed = _speed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == _player)
            _playerMovement.Speed = _defaultSpeed;
    }
}
