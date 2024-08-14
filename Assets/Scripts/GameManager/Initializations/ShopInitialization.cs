using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopInitialization : MonoBehaviour
{
    private void Awake()
    {
        //EquippedSkins.ShopSlotInitialization();

        Dictionary<string, GameObject> shopSlotsTemp = EquippedSkins.shopSlots.ToDictionary(entry => entry.Key, entry => entry.Value);

        foreach (KeyValuePair<string, GameObject> kvp in EquippedSkins.shopSlots)
        {
            if (kvp.Value == null)
            {
                if (kvp.Key == "ball")
                {
                    Transform categoryMenu = GameObject.Find("BallsCategoryMenu").transform;
                    shopSlotsTemp[kvp.Key] = FindDeepChild(categoryMenu, "Default").gameObject;
                    shopSlotsTemp[kvp.Key].transform.Find("EquippedSkinText").gameObject.SetActive(true);
                }
                
                else if (kvp.Key == "block")
                {
                    Transform categoryMenu = GameObject.Find("BlocksCategoryMenu").transform;
                    shopSlotsTemp[kvp.Key] = FindDeepChild(categoryMenu, "Default").gameObject;
                    shopSlotsTemp[kvp.Key].transform.Find("EquippedSkinText").gameObject.SetActive(true);
                }
                
                else if (kvp.Key == "background")
                {
                    Transform categoryMenu = GameObject.Find("BackgroundsCategoryMenu").transform;
                    shopSlotsTemp[kvp.Key] = FindDeepChild(categoryMenu, "Default").gameObject;
                    shopSlotsTemp[kvp.Key].transform.Find("EquippedSkinText").gameObject.SetActive(true);
                }
            }
        }

        EquippedSkins.shopSlots = shopSlotsTemp;
    }

    private Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindDeepChild(child, name);

            if (result != null) 
                return result;
        }
        
        return null;
    }

    public static void LoadData()
    {
        EquippedSkins.ShopSlotInitialization();
        Skins.instance.Load();

        foreach (string skin in Skins.instance.skinsData.boughtSkins)
        {
            GameObject boughtSkin = GameObject.Find(skin);
            boughtSkin.tag = "BoughtSkin";
            boughtSkin.GetComponent<Skin>().CheckIsBought();
        }

        Dictionary<string, string> equippedSkinName = Skins.instance.skinsData.equippedSkins.ToDictionary(entry => entry.Key, entry => entry.Value);

        foreach (KeyValuePair<string, string> kvp in equippedSkinName)
        {
            GameObject equippedSkin;

            if (kvp.Value == ""  || kvp.Value == null)
            {
                string nameDefaultSkin = $"Default{char.ToUpper(kvp.Key[0])}{kvp.Key.Substring(1)}";
                Skins.instance.skinsData.equippedSkins[kvp.Key] = nameDefaultSkin;
                equippedSkin = GameObject.Find(nameDefaultSkin);
            }
             
            else
                equippedSkin = GameObject.Find(kvp.Value);

            Material equippedSkinMaterial = equippedSkin.GetComponent<Skin>().skin;
        
            if (kvp.Key == "ball")
                EquippedSkins.ChangeSelectedSkin("ball", equippedSkin, equippedSkinMaterial);

            else if (kvp.Key == "block")
                EquippedSkins.ChangeSelectedSkin("block", equippedSkin, equippedSkinMaterial);

            else if (kvp.Key == "background")
                EquippedSkins.ChangeSelectedSkin("background", equippedSkin, equippedSkinMaterial);
        }
    }
}
