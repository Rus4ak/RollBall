using System.Collections.Generic;
using UnityEngine;

public class ShopInitialization : MonoBehaviour
{
    private void Start()
    {
        if (EquippedSkins.shopSlot == null)
            EquippedSkins.shopSlot = GameObject.Find("Default");

        EquippedSkins.shopSlot.transform.Find("EquippedSkinText").gameObject.SetActive(true);
    }

    public static void LoadData()
    {
        Skins.instance.Load();

        foreach (string skin in Skins.instance.skinsData.boughtSkins)
        {
            GameObject boughtSkin = GameObject.Find(skin);
            boughtSkin.tag = "BoughtSkin";
            boughtSkin.GetComponent<Skin>().CheckIsBought();
        }

        string equippedSkinName = Skins.instance.skinsData.equippedSkin;
        
        if (equippedSkinName == ""  || equippedSkinName == null)
            equippedSkinName = "Default";

        GameObject equippedSkin = GameObject.Find(equippedSkinName);
        
        Material equippedSkinMaterial = equippedSkin.GetComponent<Skin>().skin;
        
        EquippedSkins.ChangeSelectedBallSkin(equippedSkin, equippedSkinMaterial);
    }
}
