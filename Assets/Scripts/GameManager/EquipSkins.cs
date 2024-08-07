using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSkins : MonoBehaviour
{
    private GameObject[] blocks;

    void Start()
    {
        blocks = GameObject.FindGameObjectsWithTag("ForSkin");

        foreach (GameObject block in blocks)
        {
            block.GetComponent<Renderer>().material = EquippedSkins.skinMaterials["block"];
        }

        RenderSettings.skybox = EquippedSkins.skinMaterials["background"];
    }
}
