using UnityEngine;

public class HittingHammer : MonoBehaviour
{
    [SerializeField] private float _force;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // The player is thrown to the side opposite to the side of the collision with the object
            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 collisionDirection = contact.normal;
                collisionDirection.x = -collisionDirection.x;
                collisionDirection.z = 0;

                collision.rigidbody.velocity = collisionDirection * _force;
            }
        }
    }
}
