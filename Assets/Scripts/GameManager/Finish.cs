using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private int _currentLevel;

    [Header("Reward")]
    [SerializeField] private GameObject _box;
    [SerializeField] private int _minCountCoins;
    [SerializeField] private int _maxCountCoins;

    [SerializeField] private bool _isDropSkin = false;

    [Header("GameMode")]
    [SerializeField] private bool _isNormalMode;
    [SerializeField] private bool _isMiniGamesMode;

    private GameObject _joystickUI;

    [NonSerialized] public int lastCompletedNormalLevel;
    [NonSerialized] public int lastCompletedMiniGamesLevel;

    public int CurrentLevel { get => _currentLevel; }
    public int MinCountCoins { get => _minCountCoins; }
    public int MaxCountCoins { get => _maxCountCoins; }
    public bool IsDropSkin { get => _isDropSkin; }

    private void Start()
    {
        _joystickUI = GameObject.FindGameObjectWithTag("Joystick");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_isNormalMode)
                FinishNormalMode();

            else if (_isMiniGamesMode)
                FinishMiniGamesMode();

            else
                throw new Exception("None of the game modes are connected to the finish");

            _finishMenu.SetActive(true);

            if (_joystickUI)
                _joystickUI.SetActive(false);

            _box.SetActive(true);

            collision.gameObject.GetComponent<Sounds>().StopRollingSound();
            collision.gameObject.SetActive(false);
        }
    }

    private void FinishNormalMode()
    {
        lastCompletedNormalLevel = LevelsController.lastCompletedNormalLevel;

        if (LevelsController.lastCompletedNormalLevel < CurrentLevel)
            LevelsController.lastCompletedNormalLevel = CurrentLevel;

        Progress.Instance.progressData.completedNormalLevels = LevelsController.lastCompletedNormalLevel;
        Progress.Instance.Save();
    }

    private void FinishMiniGamesMode()
    {
        lastCompletedMiniGamesLevel = LevelsController.lastCompletedMiniGamesLevel;

        if (LevelsController.lastCompletedMiniGamesLevel < CurrentLevel)
            LevelsController.lastCompletedMiniGamesLevel = CurrentLevel;

        Progress.Instance.progressData.completedMiniGamesLevels = LevelsController.lastCompletedMiniGamesLevel;
        Progress.Instance.Save();
    }
}
