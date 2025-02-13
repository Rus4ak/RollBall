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

    [Header("Stars")]
    [SerializeField] private float _timeTwoStars;
    [SerializeField] private float _timeThreeStars;

    private GameObject _joystickUI;
    private IARManager _IARManager;

    private int _countStars = 1;

    public static float passingTime = 0;

    [NonSerialized] public int lastCompletedNormalLevel;
    [NonSerialized] public int lastCompletedMiniGamesLevel;

    public int CurrentLevel { get => _currentLevel; }
    public int MinCountCoins { get => _minCountCoins; }
    public int MaxCountCoins { get => _maxCountCoins; }
    public bool IsDropSkin { get => _isDropSkin; }
    public float TimeTwoStars { get => _timeTwoStars; }
    public float TimeThreeStars { get => _timeThreeStars; }
    public int CountStars {  get => _countStars; }
    public GameObject Box {  get => _box; }

    private void Start()
    {
        _joystickUI = GameObject.FindGameObjectWithTag("Joystick");

        try
        {
            _IARManager = GameObject.FindGameObjectWithTag("IARManager").GetComponent<IARManager>();
        }
        catch
        {
            _IARManager = null;
        }
    }

    private void Update()
    {
        passingTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_isNormalMode)
            {
                RewardStars();
                FinishNormalMode();
            }

            else if (_isMiniGamesMode)
                FinishMiniGamesMode();

            else
                throw new Exception("None of the game modes are connected to the finish");

            _finishMenu.SetActive(true);

            if (_joystickUI)
                _joystickUI.SetActive(false);

            collision.gameObject.GetComponent<Sounds>().StopRollingSound();
            collision.gameObject.SetActive(false);
        }
    }

    private void FinishNormalMode()
    {
        if (_IARManager != null && CurrentLevel >= 5 && !IARManager.isShownReview)
            _IARManager.ShowReview();

        lastCompletedNormalLevel = LevelsController.lastCompletedNormalLevel;

        if (LevelsController.lastCompletedNormalLevel < CurrentLevel)
            LevelsController.lastCompletedNormalLevel = CurrentLevel;

        if (CurrentLevel != 0)
        {
            if (LevelsController.countStarsNormalMode[CurrentLevel] < _countStars)
                LevelsController.countStarsNormalMode[CurrentLevel] = _countStars;

            if (Progress.Instance.progressData.countStarsNormalMode[CurrentLevel] < _countStars)
                Progress.Instance.progressData.countStarsNormalMode[CurrentLevel] = _countStars;
        }
        else
        {
            _box.SetActive(true);
        }

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

        _box.SetActive(true);
    }

    private void RewardStars()
    {
        if (_timeTwoStars >= passingTime)
            _countStars++;

        if (_timeThreeStars >= passingTime)
            _countStars++;
    }
}
