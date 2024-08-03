using UnityEngine;

public class EquippedSkins
{
    // shopSlot and ballSkin must be dictinary
    public static GameObject shopSlot;
    public static Material ballSkin;


    static public void ChangeSelectedBallSkin(GameObject ball, Material skin)
    {
        if (shopSlot == ball)
            return;
        
        if (shopSlot != null)
        {
            shopSlot.tag = "Untagged";
            shopSlot.transform.Find("EquippedSkinText").gameObject.SetActive(false);
        }

        shopSlot = ball;
        shopSlot.tag = "EquippedSkin";
        shopSlot.transform.Find("EquippedSkinText").gameObject.SetActive(true);
        ballSkin = skin;
        Skins.instance.skinsData.equippedSkin = shopSlot.name;
    }

    //static public void ChangeSelectedBlockSkin(GameObject block, Material skin)
    //{

    //}

    //static public void ChangeSelectedBackgroundSkin(GameObject background, Material skin)
    //{

    //}
}
