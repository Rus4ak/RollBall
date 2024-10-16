using TMPro;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Material _skinMaterial;
    [SerializeField] private Sprite _skinImage;

    private GameObject _shopSlot;
    private Transform _coin;
    private Transform _priceText;
    private bool _isBought;

    public Material SkinMaterial => _skinMaterial;
    public Sprite SkinImage => _skinImage;

    private void Awake()
    {
        _shopSlot = gameObject;
        _coin = gameObject.transform.Find("Coin");
        _priceText = gameObject.transform.Find("Price");
        _priceText.GetComponent<TextMeshProUGUI>().text = _price.ToString();

        CheckIsBought();
    }

    public void CheckIsBought()
    {
        _isBought = gameObject.CompareTag("BoughtSkin");

        if (_isBought == false)
            _isBought = gameObject.CompareTag("EquippedSkin");

        // If the skin is bought, the price tag is turned off
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
            if (_price <= Bank.Instance.Coins)
            {
                Skins.Instance.skinsData.boughtSkins.Add(gameObject.name);
                gameObject.tag = "BoughtSkin";
                Bank.Instance.Coins -= _price;

                Progress.Instance.progressData.bank = Bank.Instance.Coins;
                Progress.Instance.Save();
                
                CheckIsBought();
                SkinsList.ChangeTag(gameObject.name);
            }
        }
        else
        {
            switch (ChoiceCategory.currentCategory)
            {
                case "BallsCategoryMenu":
                    EquippedSkins.ChangeSelectedSkin("ball", _shopSlot, skinMaterial: SkinMaterial);
                    CheckIsBought();
                    break;

                case "BlocksCategoryMenu":
                    EquippedSkins.ChangeSelectedSkin("block", _shopSlot, skinMaterial: SkinMaterial);
                    break;

                case "BackgroundsCategoryMenu":
                    EquippedSkins.ChangeSelectedSkin("background", _shopSlot, skinImage: SkinImage);
                    break;
            }
        }
        Skins.Instance.Save();
    }
}
