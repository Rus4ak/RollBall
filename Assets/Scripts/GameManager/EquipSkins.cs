using UnityEngine;

public static class EquipSkins
{
    public static void EquipMaterials(Material ballMaterial, Material floorMaterial)
    {
        ballMaterial.color = EquippedSkins.skinMaterials["ball"].color;
        floorMaterial.color = EquippedSkins.skinMaterials["block"].color - new Color(.15f, .15f, .15f);
    }
}
