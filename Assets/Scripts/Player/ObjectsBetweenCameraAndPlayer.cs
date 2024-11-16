using System.Collections.Generic;
using UnityEngine;

public class ObjectsBetweenCameraAndPlayer : MonoBehaviour
{
    [SerializeField] private Transform _sphere;
    [SerializeField] private Material _transparentMaterial;

    private Material _defaultMaterial;
    private Renderer _renderer;
    private Color _color;
    private Material _shaderGraphMaterial;

    private Dictionary<Renderer, Material> _defaultMaterials = new Dictionary<Renderer, Material>();

    private void Update()
    {
        Vector3 direction = _sphere.position - transform.position;

        // Raycast from the camera to the player
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, direction.magnitude))
        {
            if (hit.collider.gameObject != _sphere.gameObject)
            {
                if (_defaultMaterials.Count >= 1)
                {
                    foreach (KeyValuePair<Renderer, Material> kvp in _defaultMaterials)
                    {
                        kvp.Key.material = kvp.Value;
                    }
                    
                    _defaultMaterials.Clear();
                    _defaultMaterial = null;
                }

                if (!hit.collider.gameObject.TryGetComponent<Renderer>(out _renderer))
                    return;

                if (_renderer.material.renderQueue == (int)UnityEngine.Rendering.RenderQueue.Transparent)
                {
                    _shaderGraphMaterial = _renderer.material;
                    _shaderGraphMaterial.SetFloat("_Alpha", .5f);
                    return;
                }

                // Saving the default material
                if (_defaultMaterial == null)
                    _defaultMaterial = _renderer.sharedMaterial;
                
                // Copying the material of the object that falls under the raycast
                _transparentMaterial.CopyPropertiesFromMaterial(_renderer.material);
                _transparentMaterial.SetFloat("_Surface", 1);

                UpdateMaterialRenderQueue(_transparentMaterial);

                _color = _transparentMaterial.color;
                _color.a = .5f;

                _transparentMaterial.color = _color;

                _renderer.material = _transparentMaterial;

                _defaultMaterials.Add(_renderer, _defaultMaterial);
            }
            else
            {
                if (_shaderGraphMaterial != null)
                {
                    _shaderGraphMaterial.SetFloat("_Alpha", 1);
                    return;
                }

                if (_defaultMaterials.Count >= 1)
                {
                    _color.a = 1f;
                    _transparentMaterial.color = _color;

                    foreach (KeyValuePair<Renderer, Material> kvp in _defaultMaterials)
                    {
                        kvp.Key.material = kvp.Value;
                    }

                    _defaultMaterials.Clear();
                    _defaultMaterial = null;
                    _renderer = null;
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
