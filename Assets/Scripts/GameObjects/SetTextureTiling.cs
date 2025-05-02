using UnityEngine;

public class SetTextureTiling : MonoBehaviour
{
    [SerializeField] private bool _isTilingX = true;
    [SerializeField] private bool _isTilingZ = true;
    [SerializeField] private float _distanceX = 5f;
    [SerializeField] private float _distanceZ = 5f;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(_meshRenderer.material);
     
        Vector2 tiling = Vector2.one;

        if (_isTilingX)
            tiling.x = transform.localScale.x / _distanceX;

        if (_isTilingZ)
            tiling.y = transform.localScale.z / _distanceZ;

        material.mainTextureScale = tiling;
        _meshRenderer.material = material;
    }
}
