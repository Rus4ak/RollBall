using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;

    private void Start()
    {
        UpdateBalance();
    }

    public void UpdateBalance()
    {
        _balanceText.text = Bank.Instance.Coins.ToString();
    }
}
