using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    public void Pause()
    {
        Time.timeScale = 0;

        _pauseMenu.SetActive(true);
    }
}
