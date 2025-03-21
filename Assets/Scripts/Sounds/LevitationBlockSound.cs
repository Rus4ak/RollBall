using UnityEngine;

public class LevitationBlockSound : MonoBehaviour
{
    private AudioSource _audioSource;

    public float volume;
    public bool isChange;

    public static bool isStop = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = SoundVolume.volume * .6f;
        volume = _audioSource.volume;
        isChange = true;
    }

    private void Update()
    {
        if (PlayerMovement.isDead)
        {
            if (_audioSource.isPlaying) 
                _audioSource.Stop();
            isStop = true;
        }

        if (isChange)
        {
            if (_audioSource.volume != SoundVolume.volume * .6f)
            {
                _audioSource.volume = SoundVolume.volume * .6f;
                volume = _audioSource.volume;
            }
        }

        if (isStop)
        {
            isChange = false;
            _audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _audioSource.Play();
    }
}
