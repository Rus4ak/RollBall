using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quality : MonoBehaviour
{
    [SerializeField] private List<GameObject> _qualityButtons;

    private string _currentQuality = "High";

    public static int quality = 3;

    private void Start()
    {
        switch (quality)
        {
            case 1:
                LowQuality();
                break;

            case 2:
                MediumQuality();
                break;

            case 3:
                HighQuality();
                break;
        }
        ChangeButtonsColor();
    }

    public void LowQuality()
    {
        QualitySettings.SetQualityLevel(1, true);
        quality = 1;
        _currentQuality = "Low";
        SaveData();
        ChangeButtonsColor();
    }

    public void MediumQuality()
    {
        QualitySettings.SetQualityLevel(3, true);
        quality = 2;
        _currentQuality = "Medium";
        SaveData();
        ChangeButtonsColor();
    }

    public void HighQuality()
    {
        QualitySettings.SetQualityLevel(5, true);
        quality = 3;
        _currentQuality = "High";
        SaveData();
        ChangeButtonsColor();
    }

    private void SaveData()
    {
        Options.Instance.optionsData.quality = quality;
        Options.Instance.Save();
    }

    private void ChangeButtonsColor()
    {
        foreach (GameObject qualityButton in _qualityButtons)
        {
            if (qualityButton.name == _currentQuality)
            {
                qualityButton.GetComponent<Image>().color = Color.white;
                qualityButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(.75f, .75f, .75f, .85f);
            }
            else
            {
                qualityButton.GetComponent<Image>().color = new Color(1, 1, 1, .58f);
                qualityButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(.75f, .75f, .75f, .5f);
            }
        }
    }
}
