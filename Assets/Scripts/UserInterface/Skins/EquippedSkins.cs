using System.Collections.Generic;
using UnityEngine;

public class EquippedSkins
{
    public static Dictionary<string, GameObject> shopSlots = new Dictionary<string, GameObject>();
    public static Dictionary<string, Material> skinMaterials = new Dictionary<string, Material>();


    public static void ShopSlotInitialization()
    {
        shopSlots["ball"] = null;
        shopSlots["block"] = null;
        shopSlots["background"] = null;
    }

    public static void ChangeSelectedSkin(string skinType, GameObject skin, Material skinMaterial)
    {
        if (shopSlots[skinType] == skin)
            return;

        // Returning the equipped skin to the bought skin state
        if (shopSlots[skinType] != null)
        {
            shopSlots[skinType].tag = "BoughtSkin";
            shopSlots[skinType].transform.Find("EquippedSkinText").gameObject.SetActive(false);
        }

        // Switching the transferred skin to the equipped skin state
        shopSlots[skinType] = skin;
        shopSlots[skinType].tag = "EquippedSkin";
        shopSlots[skinType].transform.Find("EquippedSkinText").gameObject.SetActive(true);
        skinMaterials[skinType] = skinMaterial;
        Skins.Instance.skinsData.equippedSkins[skinType] = shopSlots[skinType].name;
    }
}
