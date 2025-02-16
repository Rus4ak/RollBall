using UnityEngine;

public class KeyboardClick : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = SoundVolume.volume;
    }

    private void Update()
    {
        if (_audioSource.volume != SoundVolume.volume)
        {
            _audioSource.volume = SoundVolume.volume;
        }
    }
}
