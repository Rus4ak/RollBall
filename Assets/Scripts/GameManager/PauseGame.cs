using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private AudioSource _ballRollingSound;

    private GameObject _joystickUI;

    private void Start()
    {
        _joystickUI = GameObject.FindGameObjectWithTag("Joystick");
    }

    public void Pause()
    {
        Time.timeScale = 0;

        LevitationBlockSound.isStop = true;
        _ballRollingSound.Stop();
        _pauseMenu.SetActive(true);
        _joystickUI.SetActive(false);
    }
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        _joystickUI.SetActive(true);
        _ballRollingSound.Play();
        LevitationBlockSound.isStop = false;

        Time.timeScale = 1f;
    }
}
