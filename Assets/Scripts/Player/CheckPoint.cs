using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector3 checkPoint = new Vector3(0, 1f, 0);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkPoint = collision.gameObject.transform.position;
            checkPoint.y += 1f;

            RestartGame.resetPosition = checkPoint;
        }
    }
}
