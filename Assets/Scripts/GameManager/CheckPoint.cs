using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector3 checkPoint = new Vector3(0, 1f, 0);

    // If the player touches a block with a CheckPoint tag, the checkpoint changes to the position of this block
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkPoint = collision.gameObject.transform.position;
            checkPoint.y += 1f;
        }
    }
}
