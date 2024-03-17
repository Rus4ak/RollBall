using UnityEngine;

public class ObjectsBetweenCameraAndPlayer : MonoBehaviour
{
    [SerializeField] private Transform _sphere;
    [SerializeField] private Material _transparentMaterial;

    private Renderer _renderer;
    private Material _material;
    private Renderer _lastRenderer;
    private Material _lastMaterial;
    private Color _lastMaterialColor;

    private void Start()
    {
        _material = _sphere.GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector3 direction = _sphere.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude))
        {
            if (hit.collider.gameObject != _sphere.gameObject)
            {
                _renderer = hit.collider.gameObject.GetComponent<Renderer>();
                
                if (_renderer == _lastRenderer)
                    return;

                _material = _renderer.material;
                
                Color gameObjectColor = _material.color;

                _lastRenderer = _renderer;
                _lastMaterial = _material;
                _lastMaterialColor = gameObjectColor;
                _lastMaterialColor.a = 1f;
                
                _material = _transparentMaterial;
                
                gameObjectColor.a = .2f;
                _material.color = gameObjectColor;

                _renderer.material = _material;
            }
            else
            {
                if (_lastMaterial != null)
                {
                    _lastMaterial.color = _lastMaterialColor;
                    _lastRenderer.material = _lastMaterial;
                    _lastMaterial = null;
                    _lastRenderer = null;
                }
            }
        }
    }
}
