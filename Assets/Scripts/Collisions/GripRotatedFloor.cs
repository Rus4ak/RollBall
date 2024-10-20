using UnityEngine;

public class GripRotatedFloor : MonoBehaviour
{
    private Transform _playerParent;

    private void Start()
    {
        _playerParent = GameObject.FindGameObjectWithTag("Player").transform.parent.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.transform.parent.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            // If the player's parent is equal to this GameObject, the player's parent becomes _playerParent
            if (collision.gameObject.transform.parent.parent.gameObject == gameObject)
                collision.gameObject.transform.parent.SetParent(_playerParent);
    }
}
