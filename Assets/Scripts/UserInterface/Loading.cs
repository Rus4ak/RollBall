using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPrefab;

    private GameObject _loading;

    public void StartLoading()
    {
        _loading = Instantiate(_loadingPrefab, transform.parent);
        _loading.transform.position = transform.position;
    }

    public void StopLoading()
    {
        Destroy(_loading);
    }
}
