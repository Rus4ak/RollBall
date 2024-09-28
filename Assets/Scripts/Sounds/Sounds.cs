using UnityEngine;
using UnityEngine.UIElements;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource _rolling;
    [SerializeField] private AudioSource _hit;
    [SerializeField] private AudioSource _music;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _music.volume = Mathf.Lerp(0, .05f, MusicVolume.volume);
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

        _rolling.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        _rolling.Stop();
    }

    private void Update()
    {
        // Changing the volume of the movement sound depending on the speed of movement
        float rollingVolume = Mathf.Lerp(0, 1, _rb.velocity.magnitude / 10);

        _rolling.volume = Mathf.Lerp(0, rollingVolume, SoundVolume.volume);
        _rolling.pitch = Mathf.Lerp(.25f, .45f, _rb.velocity.magnitude / 15);

        if (MusicVolume.isChanged)
        {
            _music.volume = Mathf.Lerp(0, .5f, MusicVolume.volume);
        }
    }
}
