using UnityEngine;

public class LookAtPlayer : MonoBehaviour
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

        if (cameraOffset.z <= 2f)
            cameraOffset.z = 2f;

        Vector3 pos = _sphere.position - cameraOffset;

        pos.y += 2.5f;
        pos.z -= 3f;

        pos.y = Mathf.Max(pos.y, _sphere.position.y - 5f);

        return pos;
    }
}
