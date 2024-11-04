using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _defaultSpeed;
    private PlayerMovement _playerMovement;
    
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _defaultSpeed = _playerMovement.Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpeedBlock"))
            // Saving the player's movement speed and changing it to the specified speed
            _playerMovement.Speed = _speed;

        else
            // Changing the player's movement speed to the default speed
            _playerMovement.Speed = _defaultSpeed;
    }
}
