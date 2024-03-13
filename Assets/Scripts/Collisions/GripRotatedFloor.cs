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
            if (collision.gameObject.transform.parent.parent.gameObject == this.gameObject)
                collision.gameObject.transform.parent.SetParent(null);
    }
}
