using Unity.VisualScripting;
using UnityEngine;

public class BoxSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _boxStanding;
    [SerializeField] private AudioSource _boxOpening;

    private void Start()
    {
        _boxOpening.volume = SoundVolume.volume;
        _boxStanding.volume = SoundVolume.volume;
    }

    public void BoxStandingSoundPlay()
    {
        RandomSound(ref _boxStanding);
        _boxStanding.Play();
    }

    public void BoxOpeningSoundPlay()
    {
        RandomSound(ref _boxOpening);
        _boxOpening.Play();
    }

    private void RandomSound(ref AudioSource source)
    {
        source.pitch = Random.Range(.9f, 1.1f);
        source.panStereo = Random.Range(-0.5f, .5f);
    }
}
