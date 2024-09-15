using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private GameObject _joystickUI;

    private void Start()
    {
        _joystickUI = GameObject.FindGameObjectWithTag("Joystick");
    }

    public void Pause()
    {
        Time.timeScale = 0;

        _pauseMenu.SetActive(true);
        _joystickUI.SetActive(false);
    }
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        _joystickUI.SetActive(true);

        Time.timeScale = 1f;
    }
}
