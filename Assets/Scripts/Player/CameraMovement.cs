using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _sphere;
    [SerializeField] private float smoothTime = 0.5f;

    private Rigidbody _sphereRigidbody;

    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _sphereRigidbody = _sphere.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = CalculateTargetPosition();

        // Smooth movement of the camera from the current position to the new one
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);

        transform.position = newPosition;
        transform.LookAt(_sphere);
    }

    // The camera moves to the opposite position from the player's movement direction, maintaining a certain distance
    private Vector3 CalculateTargetPosition()
    {
        Vector3 cameraOffset = _sphereRigidbody.velocity.normalized * 1.5f;

        // The distance of the camera to the player in Z cannot be less than the specified value
        if (cameraOffset.z <= 5.5f)
            cameraOffset.z = 5.5f;

        Vector3 pos = _sphere.position - cameraOffset;

        pos.y += 12f;
        pos.z -= 8f;

        pos.y = Mathf.Max(pos.y, _sphere.position.y - 5f);

        return pos;
    }
}
