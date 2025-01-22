using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChoiceLevel : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonClickSource;
    [SerializeField] private GameObject _loadingMenu;

    private int _quality;
    private float _soundVolume;
    private float _musicVolume;

    public void SceneLoad(string sceneName)
    {
        Instantiate(_loadingMenu);
        StartCoroutine(PlaySoundAndLoadScene(sceneName));
    }

    // Play the sound of the button click in parallel with the scene change settings
    IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        _buttonClickSource.Play();

        if (Time.timeScale == 0)
            Time.timeScale = 1;

        yield return new WaitForSeconds(_buttonClickSource.clip.length);

        _quality = Quality.quality;
        _soundVolume = SoundVolume.volume;
        _musicVolume = MusicVolume.volume;
        
        SceneManager.LoadSceneAsync(sceneName);

        CheckPoint.checkPoint = new Vector3(0, 1f, 0);
        PlayerMovement.spawnPosition = new Vector3(0, 1f, 0);
        Quality.quality = _quality;
        SoundVolume.volume = _soundVolume;
        MusicVolume.volume = _musicVolume;

        QualitySettings.SetQualityLevel(_quality, true);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
