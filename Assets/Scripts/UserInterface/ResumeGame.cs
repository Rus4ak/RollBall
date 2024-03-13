using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    public void Resume()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;
    }
}
