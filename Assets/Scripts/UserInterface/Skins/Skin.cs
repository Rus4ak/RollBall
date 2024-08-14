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

                Progress.instance.progressData.bank = Bank.instance.coins;
                Progress.instance.Save();
                
                CheckIsBought();
            }
        }
        else
        {
            switch (ChoiceCategory.currentCategory)
            {
                case "BallsCategoryMenu":
                    EquippedSkins.ChangeSelectedSkin("ball", _shopSlot, _skin);
                    CheckIsBought();
                    break;

                case "BlocksCategoryMenu":
                    EquippedSkins.ChangeSelectedSkin("block", _shopSlot, _skin);
                    break;

                case "BackgroundsCategoryMenu":
                    EquippedSkins.ChangeSelectedSkin("background", _shopSlot, _skin);
                    break;
            }
        }
        Skins.instance.Save();
    }
}
