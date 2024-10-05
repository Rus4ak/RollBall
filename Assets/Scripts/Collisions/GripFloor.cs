using UnityEngine;

public class GripFloor : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _lastPos;
    private bool _isStart = false;

    private void FixedUpdate()
    {
        if (_isStart)
        {
            // The position between the current position and the position in the previous frame
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
