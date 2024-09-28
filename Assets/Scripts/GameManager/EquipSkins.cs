using UnityEngine;

public class EquipSkins : MonoBehaviour
{
    [SerializeField] private Material ballMaterial;
    [SerializeField] private Material blockMaterial;

    // Player and block materials copy properties from equipped skins
    private void Awake()
    {
        ballMaterial.CopyPropertiesFromMaterial(EquippedSkins.skinMaterials["ball"]);
        blockMaterial.CopyPropertiesFromMaterial(EquippedSkins.skinMaterials["block"]);
    }
}
