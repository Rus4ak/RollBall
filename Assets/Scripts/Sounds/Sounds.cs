using UnityEngine;
using UnityEngine.UIElements;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource _rolling;
    [SerializeField] private AudioSource _hit;
    [SerializeField] private AudioSource _music;

    private Rigidbody _rb;
    private bool _isSoundRollingStop;

    private void Start()
    {
        _isSoundRollingStop = false;
        _rb = GetComponent<Rigidbody>();

        _music.volume = Mathf.Lerp(0, .08f, MusicVolume.volume);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Playback of the hit sound depending on the force of the hit
        if (collision.contacts[0].impulse.magnitude > 2f)
        {
            float hitVolume = Mathf.Lerp(0, .7f, collision.contacts[0].impulse.magnitude / 25);

            _hit.volume = Mathf.Lerp(0, hitVolume, SoundVolume.volume);
            _hit.pitch = Random.Range(.8f, 1.2f);
            _hit.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Playback of the movement sound
        if (_rolling.isPlaying)
            return;

        if (!_isSoundRollingStop)
            _rolling.Play();
    }

    private void Update()
    {
        // Changing the volume of the movement sound depending on the speed of movement
        float rollingVolume = Mathf.Lerp(0, 1, _rb.velocity.magnitude / 8);

        _rolling.volume = Mathf.Lerp(0, rollingVolume, SoundVolume.volume);
        _rolling.pitch = Mathf.Lerp(.25f, .45f, _rb.velocity.magnitude / 15);

        if (_music.volume != MusicVolume.volume)
        {
            _music.volume = Mathf.Lerp(0, .08f, MusicVolume.volume);
        }

        // If there is nothing under the player, the rolling sound is stopped
        if (!Physics.Raycast(transform.position, Vector3.down, 7f))
            _rolling.Stop();
    }

    public void StopRollingSound()
    {
        _isSoundRollingStop = true;
        _rolling.Stop();
    }
}
