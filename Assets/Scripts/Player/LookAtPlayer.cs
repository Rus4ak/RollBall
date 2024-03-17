using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform _sphere;

    private Rigidbody _sphereRigidbody;

    private void Start()
    {
        _sphereRigidbody = _sphere.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.LookAt(_sphere);

        Vector3 cameraOffset = _sphereRigidbody.velocity.normalized * 1.3f;

        if (cameraOffset.z <= 1f)
            cameraOffset.z = 1f;

        Vector3 pos = _sphere.position - cameraOffset;

        pos.y += 3f;
        pos.z -= 3f;

        Vector3 newPos = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        newPos.y = Mathf.Max(newPos.y, _sphere.position.y - 5f);

        transform.position = newPos;
    }
}
