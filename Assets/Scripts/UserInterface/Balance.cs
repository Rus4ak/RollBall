using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;

    static public TextMeshProUGUI balanceText;

    private void Start()
    {
        balanceText = _balanceText;
        UpdateBalance();
    }

    static public void UpdateBalance()
    {
        balanceText.text = Bank.Instance.Coins.ToString();
    }
}
