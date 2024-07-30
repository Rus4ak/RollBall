using TMPro;
using UnityEngine;

public class Quality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    public static int quality = 2;

    private void Start()
    {
        switch (quality)
        {
            case 1:
                _dropdown.value = 2;
                break;

            case 2:
                _dropdown.value = 1;
                break;

            case 3:
                _dropdown.value = 0;
                break;
        }
    }

    public void OnClick()
    {
        int index = _dropdown.value;
        quality = index;
        
        switch (index)
        {
            case 0:
                HighQuality();
                break;
            
            case 1:
                MediumQuality(); 
                break;

            case 2:
                LowQuality();
                break;
        }

        Options.instance.optionsData.quality = quality;
        Options.instance.Save();
    }

    private void LowQuality()
    {
        QualitySettings.SetQualityLevel(1, true);
        quality = 1;
    }

    private void MediumQuality()
    {
        QualitySettings.SetQualityLevel(3, true);
        quality = 2;
    }

    private void HighQuality()
    {
        QualitySettings.SetQualityLevel(5, true);
        quality = 3;
    }
}
