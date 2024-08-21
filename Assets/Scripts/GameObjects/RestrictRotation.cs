using UnityEngine;

public class RestrictRotation : MonoBehaviour
{
    [SerializeField][Range(-1, 1)] private float _maxRotation;

    private void FixedUpdate()
    {
        Quaternion currentRotation = transform.rotation;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -_maxRotation, _maxRotation);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -_maxRotation, _maxRotation);
        
        transform.rotation = currentRotation;
    }
}
