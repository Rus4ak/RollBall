using UnityEngine;
using UnityEngine.SceneManagement;


public class ChoiceLevel : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        CheckPoint.checkPoint = new Vector3(0, 1f, 0);
        RestartGame.resetPosition = new Vector3(0, 1f, 0);
        Time.timeScale = 1.0f;
    }
}
