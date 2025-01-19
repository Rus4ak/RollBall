using UnityEngine;

public class GripRotatedFloor : MonoBehaviour
{
    private Transform _playerParent;
    private Transform _player;

    private void Start()
    {
        _playerParent = GameObject.FindGameObjectWithTag("Player").transform.parent.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.transform.parent;
            _player.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            // If the player's parent is equal to this GameObject, the player's parent becomes _playerParent
            if (_player.parent.gameObject == gameObject)
                _player.SetParent(_playerParent);
    }

    private void OnDestroy()
    {
        if (_player != null)
            if (_player.parent != _playerParent)
                _player.SetParent(_playerParent);
    }
}
