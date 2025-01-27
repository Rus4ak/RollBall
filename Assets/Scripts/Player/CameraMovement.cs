using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _sphere;

    [SerializeField] private float _smoothTime = 0.5f;
    [SerializeField] private float _distanceBetweenPlayer = 1f;

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
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);

        transform.position = newPosition;
        transform.LookAt(_sphere);
    }

    // The camera moves to the opposite position from the player's movement direction, maintaining a certain distance
    private Vector3 CalculateTargetPosition()
    {
        Vector3 cameraOffset = _sphereRigidbody.velocity.normalized * 2f;

        // The distance of the camera to the player in Z cannot be less than the specified value
        if (cameraOffset.z <= 7f * _distanceBetweenPlayer)
            cameraOffset.z = 7f * _distanceBetweenPlayer;

        Vector3 pos = _sphere.position - cameraOffset;

        pos.y += 10f * _distanceBetweenPlayer;
        pos.z -= 12f * _distanceBetweenPlayer;

        pos.y = Mathf.Max(pos.y, _sphere.position.y - 5f);

        return pos;
    }
}
