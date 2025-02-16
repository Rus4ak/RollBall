using UnityEngine;

public class JumpBlockSound : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = SoundVolume.volume * .1f;
    }

    private void Update()
    {
        if (_audioSource.volume != SoundVolume.volume * .1f)
        {
            _audioSource.volume = SoundVolume.volume * .1f;
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
