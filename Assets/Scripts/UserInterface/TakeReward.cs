using TMPro;
using UnityEngine;

public class TakeReward : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _spawnCoin;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _background;

    private void Start()
    {
        _text.text = Finish.countCoins.ToString();
        Bank.instance.coins += Finish.countCoins;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            InstantiateCoins();
            gameObject.SetActive(false);
            _background.SetActive(false);
        }
    }

    private void InstantiateCoins()
    {
        Vector3 posSpawnCoin = _spawnCoin.transform.position;

        for (int i = 0; i < Finish.countCoins; i++)
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
