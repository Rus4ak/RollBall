using UnityEngine;

public class DeadBlock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayerMovement.isDead = true;
    }
}
