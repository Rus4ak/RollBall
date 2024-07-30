using TMPro;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Material _skin;

    private GameObject _shopSlot;
    private Transform _coin;
    private Transform _priceText;
    private bool _isBought;

    public Material skin => _skin;

    private void Awake()
    {
        _shopSlot = gameObject;
        _coin = gameObject.transform.Find("Coin");
        _priceText = gameObject.transform.Find("Price");
        _priceText.GetComponent<TextMeshProUGUI>().text = _price.ToString();

        //if (EquippedSkins.shopSlot == null)
        //    if (_shopSlot.name == "Default")
        //        EquippedSkins.shopSlot = _shopSlot;
    }

    public void CheckIsBought()
    {
        _isBought = gameObject.CompareTag("BoughtSkin");

        if (_isBought == false)
            _isBought = gameObject.CompareTag("EquippedSkin");

        if (_isBought)
        {
            _coin.gameObject.SetActive(false);
            _priceText.gameObject.SetActive(false);
        }
    }

    public void ClickProcessing()
    {
        if (!_isBought)
        {
            if (_price <= Bank.instance.coins)
            {
                Skins.instance.skinsData.boughtSkins.Add(gameObject.name);
                gameObject.tag = "BoughtSkin";
                Bank.instance.coins -= _price;
                CheckIsBought();
            }
        }
        else
        {
            switch (ChoiceCategory.currentCategory)
            {
                case "BallsCategoryMenu":
                    EquippedSkins.ChangeSelectedBallSkin(_shopSlot, _skin);
                    CheckIsBought();
                    break;

                //case "BlocksCategoryMenu":
                //    EquippedSkins.ChangeSelectedBlockSkin(_shopSlot, _skin);
                //    break;

                //case "BackgroundsCategoryMenu":
                //    EquippedSkins.ChangeSelectedBackgroundSkin(_shopSlot, _skin);
                //    break;
            }
        }
        Skins.instance.Save();
    }
}
