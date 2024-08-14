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
            Renderer blockRenderer = block.GetComponent<Renderer>();
            blockRenderer.material = EquippedSkins.skinMaterials["block"];
            blockRenderer.material.shader = Shader.Find("Standard");
            blockRenderer.material.color = new Color(blockRenderer.material.color.r - .15f, blockRenderer.material.color.g - .15f, blockRenderer.material.color.b - .15f);
        }

        RenderSettings.skybox = EquippedSkins.skinMaterials["background"];
    }
}
