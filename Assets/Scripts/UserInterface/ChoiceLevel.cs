using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChoiceLevel : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonClickSource;

    private int _quality;
    private float _soundVolume;
    private float _musicVolume;

    public void SceneLoad(string sceneName)
    {
        StartCoroutine(PlaySoundAndLoadScene(sceneName));
    }

    IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        _buttonClickSource.Play();

        if (Time.timeScale == 0)
            Time.timeScale = 1;

        yield return new WaitForSeconds(_buttonClickSource.clip.length);

        _quality = Quality.quality;
        _soundVolume = SoundVolume.volume;
        _musicVolume = MusicVolume.volume;
        
        SceneManager.LoadScene(sceneName);

        CheckPoint.checkPoint = new Vector3(0, 1f, 0);
        PlayerMovement.spawnPosition = new Vector3(0, 1f, 0);
        Quality.quality = _quality;
        SoundVolume.volume = _soundVolume;
        MusicVolume.volume = _musicVolume;

        QualitySettings.SetQualityLevel(_quality, true);
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
    }
}
