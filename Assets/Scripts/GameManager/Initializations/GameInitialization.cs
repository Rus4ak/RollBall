using System.Collections.Generic;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menus;
    [SerializeField] private Material _ballMaterial;
    [SerializeField] private Material _floorMaterial;

    private void Awake()
    {
        Options.instance.Load();
        Progress.instance.Load();
    }

    private void Start()
    {
        ShopInitialization.LoadData();

        foreach (GameObject menu in _menus)
            menu.SetActive(false);
    }
}
