using UnityEngine;

public class SplashWaterSound : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = SoundVolume.volume * .5f;
    }

    private void Update()
    {
        if (_audioSource.volume != SoundVolume.volume * .5f)
        {
            _audioSource.volume = SoundVolume.volume * .5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _audioSource.Play();
        }
    }
}
