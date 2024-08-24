using UnityEngine;

public class EquipSkins : MonoBehaviour
{
    [SerializeField] private Material ballMaterial;
    [SerializeField] private Material blockMaterial;

    private void Awake()
    {
        ballMaterial.CopyPropertiesFromMaterial(EquippedSkins.skinMaterials["ball"]);
        blockMaterial.CopyPropertiesFromMaterial(EquippedSkins.skinMaterials["block"]);
    }
}
