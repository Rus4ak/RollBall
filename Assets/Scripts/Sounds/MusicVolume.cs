using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    private Slider _slider;

    public static float volume = .25f;
    public static bool isChanged = false;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.value = volume;
    }

    public void OnValueChanged()
    {
        volume = _slider.value;

        isChanged = true;

        Options.Instance.optionsData.musicVolume = volume;
        Options.Instance.Save();
    }
}
