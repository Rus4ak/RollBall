using UnityEngine;

public class JumpingFloor : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private float _force;

    private void OnCollisionEnter(Collision collision)
    {
        // The player is thrown to the side opposite to the top of the object
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 objectRotation = transform.rotation.eulerAngles;
            Vector3 direction = new Vector3(0, 1f, 0);

            // Calculating the perpendicular side to the top side of the object, depending on its rotation
            if (objectRotation.z > 0)
            {
                if (objectRotation.z < 180f)
                    direction.x = -objectRotation.z / 90f;
                else
                    direction.x = (360f - objectRotation.z) / 90f;
            }

            if (objectRotation.x > 0)
            {
                if (objectRotation.x < 180f)
                    direction.z = -objectRotation.x / 90f;
                else
                    direction.z = (360f - objectRotation.x) / 90f;
            }

            _playerRigidbody.velocity = direction * _force;
        }
    }
}
