using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;

    private void Start()
    {
        _balanceText.text = Bank.instance.coins.ToString();
    }
}
