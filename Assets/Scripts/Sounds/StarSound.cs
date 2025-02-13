using UnityEngine;

public class StarSound : MonoBehaviour
{
    [SerializeField] private AudioSource _starFlySound;
    [SerializeField] private AudioSource _starStopSound;

    private void Awake()
    {
        _starFlySound.volume = SoundVolume.volume * 0.8f;
        _starStopSound.volume = SoundVolume.volume;
    }
}
