using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinsDictionary
{
    public string skin;
    public int price;
    public Sprite sprite;
    
    [NonSerialized] public string tag;
    [NonSerialized] public bool isTagChanged;

    public SkinsDictionary(string skin, int price, Sprite sprite)
    {
        this.skin = skin;
        this.price = price;
        this.sprite = sprite;

        isTagChanged = false;
    }
}

public class SkinsList : MonoBehaviour
{
    [SerializeField] private List<SkinsDictionary> _skins;
    [SerializeField] private ChoiceCategory _choiceCategory;

    private static ChoiceCategory _choiceCategoryInstance;

    public static List<SkinsDictionary> skins;

    private void Awake()
    {
        skins ??= _skins;
        _choiceCategoryInstance = _choiceCategory;
    }

    public static void SkinsDictionaryInitialization()
    {
        foreach (SkinsDictionary skin in skins)
        {
            skin.tag ??= GameObject.Find(skin.skin).tag;

            if (skin.isTagChanged)
            {
                GameObject rewardSkin = GameObject.Find(skin.skin);
                rewardSkin.tag = skin.tag;
                Skins.Instance.skinsData.boughtSkins.Add(rewardSkin.name);
                Skins.Instance.Save();
                rewardSkin.GetComponent<Skin>().CheckIsBought();
                skin.isTagChanged = false;
            }
        }
        _choiceCategoryInstance.ActivateCategoryMenu("BallsCategoryMenu");
    }

    public static void ChangeTag(string skinName)
    {
        GameObject boughtSkin = GameObject.Find(skinName);
        
        foreach (SkinsDictionary skin in skins)
            if (skin.skin == skinName)
                skin.tag = boughtSkin.tag;
    }
}
