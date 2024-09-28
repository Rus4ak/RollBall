using UnityEngine;

public class GripFloor : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _lastPos;
    private bool _isStart = false;

    private void FixedUpdate()
    {
        // If the player is standing on a block,
        // the vector of the block's movement is added to its position.
        if (_isStart)
        {
            Vector3 tempPos = transform.position - _lastPos;

            _player.position += tempPos;
            _lastPos = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isStart = true;
            _lastPos = transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _isStart = false;
    }
}
