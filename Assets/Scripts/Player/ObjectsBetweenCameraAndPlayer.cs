using UnityEngine;

public class ObjectsBetweenCameraAndPlayer : MonoBehaviour
{
    [SerializeField] private Transform _sphere;
    
    private Renderer _renderer;
    private Color _color;

    void Update()
    {
        Vector3 direction = _sphere.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude))
        {
            if (hit.collider.gameObject != _sphere.gameObject)
            {
                _renderer = hit.collider.gameObject.GetComponent<Renderer>();

                _color = _renderer.material.color;
                _color.a = .5f;
                
                _renderer.material.color = _color;
            }
            else
            {
                _color.a = 1f;
                _renderer.material.color = _color;
            }
        }
    }
}
