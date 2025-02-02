using UnityEngine;

public class BuySkinConfirmPanel : MonoBehaviour
{
    private Skin _skin;

    public static BuySkinConfirmPanel Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance.CancelBuySkin();
            Instance = this;
        }
    }

    private void Start()
    {
        _skin = transform.parent.parent.GetComponent<Skin>();
    }

    public void BuySkin()
    {
        _skin.BuySkin();
    }

    public void CancelBuySkin()
    {
        _skin.CancelBuySkin();
        Destroy(gameObject);
    }
}
