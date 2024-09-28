using UnityEngine;

public class GripRotatedFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.transform.parent.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            // If the player's parent is equal to this GameObject, the player's parent is set to null
            if (collision.gameObject.transform.parent.parent.gameObject == this.gameObject)
                collision.gameObject.transform.parent.SetParent(null);
    }
}
