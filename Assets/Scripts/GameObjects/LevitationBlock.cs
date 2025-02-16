using UnityEngine;

public class LevitationBlock : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _time;

    private AudioSource _audioSource;
    private LevitationBlockSound _sound;
    private Rigidbody _playerRigidbody;
    private float _startCollisionTime;
    
    private bool _isStart = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _sound = GetComponent<LevitationBlockSound>();
    }

    private void Update()
    {
        if (_isStart)
        {
            if (!_audioSource.isPlaying)
            {
                _sound.isChange = false;
                _audioSource.volume = 0;
                _audioSource.Play();
            }

            if (_audioSource.volume < _sound.volume)
            {
                _audioSource.volume += Time.deltaTime;
            }
            else if (_audioSource.volume >= _sound.volume && !_sound.isChange) 
            {
                _sound.isChange = true;
            }

            // After a certain time has elapsed, the player's gravity turns on
            if (Time.time - _startCollisionTime > _time)
            {
                _playerRigidbody.useGravity = true;
                _isStart = false;
            }
        }

        if (_audioSource.isPlaying && !_isStart)
        {
            if (_sound.isChange) 
                _sound.isChange = false;

            _audioSource.volume -= Time.deltaTime;

            if (_audioSource.volume <= 0)
            {
                _sound.isChange = true;
                _audioSource.Stop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            _startCollisionTime = Time.time;

            // The player's gravity turns off
            _playerRigidbody.useGravity = false;
            // The player's velocity in Y direction increases by a specified force
            _playerRigidbody.velocity += Vector3.up * _force;

            _isStart = true;
        }
    }
}
