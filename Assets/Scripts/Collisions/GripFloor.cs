using UnityEngine;

public class GripFloor : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _lastPos;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform == _player)
            Move();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == _player)
            Move();
    }

    private void Move()
    {
        if (_lastPos == Vector3.zero)
            _lastPos = transform.position;

        Vector3 tempPosition = transform.position - _lastPos;
        
        if (tempPosition.y > 0)
            tempPosition.y = 0;
        
        _player.position += tempPosition;
        _lastPos = transform.position;
    }
}
