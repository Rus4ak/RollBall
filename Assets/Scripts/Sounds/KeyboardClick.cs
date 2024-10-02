using UnityEngine;

public class KeyboardClick : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = SoundVolume.volume;
    }
}
