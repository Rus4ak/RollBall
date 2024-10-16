using UnityEngine;
using UnityEngine.UI;

public class EquipSkins : MonoBehaviour
{
    [SerializeField] private Material _ballMaterial;
    [SerializeField] private Material _blockMaterial;
    [SerializeField] private Image _backgroundImage;

    // Player and block materials copy properties from equipped skins
    private void Awake()
    {
        _ballMaterial.CopyPropertiesFromMaterial(EquippedSkins.skinMaterials["ball"]);
        _blockMaterial.CopyPropertiesFromMaterial(EquippedSkins.skinMaterials["block"]);
        _backgroundImage.sprite = EquippedSkins.backgroundImage;
    }
}
