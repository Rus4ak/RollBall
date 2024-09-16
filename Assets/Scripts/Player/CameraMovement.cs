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

        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);

        transform.position = newPosition;
        transform.LookAt(_sphere);
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 cameraOffset = _sphereRigidbody.velocity.normalized * 1.5f;

        if (cameraOffset.z <= 4.5f)
            cameraOffset.z = 5.5f;

        Vector3 pos = _sphere.position - cameraOffset;

        pos.y += 7f;
        pos.z -= 6f;

        pos.y = Mathf.Max(pos.y, _sphere.position.y - 5f);

        return pos;
    }
}
