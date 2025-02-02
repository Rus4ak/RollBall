using UnityEngine;

public class ButtonClickVolume : MonoBehaviour
{
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        _audio.volume = SoundVolume.volume;
    }

    private void Update()
    {
        if (_audio.volume != SoundVolume.volume)
        {
            _audio.volume = SoundVolume.volume;
        }
    }
}
