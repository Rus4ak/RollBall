using System.Collections.Generic;
using UnityEngine;

public class EquippedSkins
{
    // shopSlot and ballSkin must be dictinary
    public static Dictionary<string, GameObject> shopSlots = new Dictionary<string, GameObject>();
    public static Dictionary<string, Material> skinMaterials = new Dictionary<string, Material>();


    static public void ShopSlotInitialization()
    {
        shopSlots["ball"] = null;
        shopSlots["block"] = null;
        shopSlots["background"] = null;
    }

    static public void ChangeSelectedSkin(string skinType, GameObject skin, Material skinMaterial)
    {
        if (shopSlots[skinType] == skin)
            return;
        
        if (shopSlots[skinType] != null)
        {
            shopSlots[skinType].tag = "Untagged";
            shopSlots[skinType].transform.Find("EquippedSkinText").gameObject.SetActive(false);
        }

        shopSlots[skinType] = skin;
        shopSlots[skinType].tag = "EquippedSkin";
        shopSlots[skinType].transform.Find("EquippedSkinText").gameObject.SetActive(true);
        skinMaterials[skinType] = skinMaterial;
        Skins.instance.skinsData.equippedSkins[skinType] = shopSlots[skinType].name;
    }
}
