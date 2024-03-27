using UnityEngine;
using UnityEngine.SceneManagement;


public class ChoiceLevel : MonoBehaviour
{
    private int quality;

    public void SceneLoad(string sceneName)
    {
        quality = Quality.quality;
        SceneManager.LoadScene(sceneName);
        
        CheckPoint.checkPoint = new Vector3(0, 1f, 0);
        RestartGame.resetPosition = new Vector3(0, 1f, 0);
        Quality.quality = quality;

        QualitySettings.SetQualityLevel(quality, true);
        Time.timeScale = 1.0f;
    }
}
