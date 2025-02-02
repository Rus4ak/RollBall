using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;

    public bool isBalanceInitialized = false;
    public TextMeshProUGUI balanceText;

    public static Balance Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isBalanceInitialized = true;
        balanceText = _balanceText;
        UpdateBalance();
    }

    public void UpdateBalance()
    {
        balanceText.text = Bank.Instance.Coins.ToString();
    }
}
