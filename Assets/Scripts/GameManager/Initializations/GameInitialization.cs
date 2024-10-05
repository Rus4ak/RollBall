using System.Collections.Generic;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menus;
    [SerializeField] private Material _ballMaterial;
    [SerializeField] private Material _floorMaterial;

    private void Awake()
    {
        Options.Instance.Load();
        Progress.Instance.Load();
    }

    // Loading data from the database, after which unnecessary windows are disabled
    private void Start()
    {
        ShopInitialization.LoadData();
        SkinsList.SkinsDictionaryInitialization();

        foreach (GameObject menu in _menus)
            menu.SetActive(false);
    }
}
