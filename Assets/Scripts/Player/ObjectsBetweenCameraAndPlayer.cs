using UnityEngine;

public class ObjectsBetweenCameraAndPlayer : MonoBehaviour
{
    [SerializeField] private Transform _sphere;
    [SerializeField] private Material _transparentMaterial;

    private Material _defaultMaterial;
    private Renderer _renderer;
    private Color _color;

    private void Update()
    {
        Vector3 direction = _sphere.position - transform.position;

        // Raycast from the camera to the player
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude))
        {
            if (hit.collider.gameObject != _sphere.gameObject)
            {
                if (!hit.collider.gameObject.TryGetComponent<Renderer>(out _renderer))
                    return;

                // Copying the material of the object that falls under the raycast
                _transparentMaterial.CopyPropertiesFromMaterial(_renderer.material);
                _transparentMaterial.SetFloat("_Surface", 1);

                UpdateMaterialRenderQueue(_transparentMaterial);

                _color = _transparentMaterial.color;
                _color.a = .5f;

                _transparentMaterial.color = _color;

                // Saving the default material
                _defaultMaterial = _renderer.material;
                _renderer.material = _transparentMaterial;
            }
            else
            {
                if (_renderer != null)
                {
                    _color.a = 1f;
                    _transparentMaterial.color = _color;

                    _renderer.material = _defaultMaterial;
                }
            }
        }
    }

    // Changing the material type to transparent
    private void UpdateMaterialRenderQueue(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}
