using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeReward : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _spawnCoin;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _coinPicture;
    [SerializeField] private GameObject _skinPicture;

    private Finish _finish;
    private int _countRewardCoins;
    private List<SkinsDictionary> _availableRewardSkins;
    private SkinsDictionary _rewardSkin;

    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _countRewardCoins = Random.Range(_finish.MinCountCoins, _finish.MaxCountCoins);

        // Check if a skin should drop from the box
        if (_finish.IsDropSkin)
        {
            // If the current level has already been completed, instead of a skin, coins are dropped,
            // with the amount being divided by 6 in comparison to the skin's value
            if (_finish.CurrentLevel < _finish.lastCompletedLevel)
            {
                _countRewardCoins /= 6;
                DropCoins();
            }

            else
            {
                _availableRewardSkins = new List<SkinsDictionary>();

                // Select skins that haven't been bought and cost a specified amount
                foreach (SkinsDictionary skin in SkinsList.skins)
                    if (skin.price >= _finish.MinCountCoins && skin.price <= _finish.MaxCountCoins && skin.tag != "BoughtSkin" && skin.tag != "EquippedSkin")
                        _availableRewardSkins.Add(skin);

                if (_availableRewardSkins.Count > 0)
                    DropSkin();

                else
                    DropCoins();
            }
        }
        else
        {
            DropCoins();
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!_finish.IsDropSkin)
                InstantiateCoins();
            
            gameObject.SetActive(false);
            _background.SetActive(false);
        }
    }

    private void DropSkin()
    {
        _rewardSkin = _availableRewardSkins[Random.Range(0, _availableRewardSkins.Count - 1)];
        _rewardSkin.tag = "BoughtSkin";
        _rewardSkin.isTagChanged = true;

        _text.gameObject.SetActive(false);
        _coinPicture.SetActive(false);
        _skinPicture.SetActive(true);

        _skinPicture.GetComponent<Image>().sprite = _rewardSkin.sprite;
    }

    private void DropCoins()
    {
        _text.text = _countRewardCoins.ToString();

        Bank.Instance.Coins += _countRewardCoins;
        Progress.Instance.progressData.bank = Bank.Instance.Coins;
        Progress.Instance.Save();
    }

    // Creating coins in a certain range, the number of which depends on the reward
    private void InstantiateCoins()
    {
        Vector3 posSpawnCoin = _spawnCoin.transform.position;

        for (int i = 0; i < _countRewardCoins; i++)
        {
            GameObject coin = Instantiate(_coin);
            coin.transform.SetParent(_spawnCoin.transform);

            Vector3 coinPosition = coin.transform.position;
            coinPosition.x = Random.Range(posSpawnCoin.x - 150, posSpawnCoin.x + 150);
            coinPosition.y = Random.Range(posSpawnCoin.y - 150, posSpawnCoin.y + 150);
            coinPosition.z = posSpawnCoin.z;
            coin.transform.position = coinPosition;
        }
    }
}
