using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform _sphere;

    private Rigidbody _sphereRigidbody;
    private Material _lastMaterial;
    private Color _lastMaterialColor;

    private void Start()
    {
        _sphereRigidbody = _sphere.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Move();
        ObjectsBetweenCameraAndPlayer();
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

    private void ObjectsBetweenCameraAndPlayer()
    {
        Vector3 direction = _sphere.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude))
        {
            if (hit.collider.gameObject != _sphere.gameObject)
            {
                Material gameObjectMaterial = hit.collider.GetComponent<Renderer>().material;
                
                Utils.MakeOpaqueMaterialTransparent(gameObjectMaterial);
                
                Color gameObjectColor = gameObjectMaterial.color;

                _lastMaterial = gameObjectMaterial;
                _lastMaterialColor = gameObjectColor;
                _lastMaterialColor.a = 1f;

                gameObjectColor.a = .2f;
                gameObjectMaterial.color = gameObjectColor;
            }
            else
            {
                if (_lastMaterial != null)
                {
                    _lastMaterial.color = _lastMaterialColor;
                    _lastMaterial = null;
                }
            }
        }
    }
}
