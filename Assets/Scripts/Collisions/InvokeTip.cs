using UnityEngine;

public class InvokeTip : MonoBehaviour
{
    [SerializeField] private GameObject _tip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _tip.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _tip.SetActive(false);
    }
}
