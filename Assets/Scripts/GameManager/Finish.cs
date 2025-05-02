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
    public bool isNormalMode;
    public bool isInvisibleMode;
    public bool isQuessMode;
    public bool isFlyMode;
    public bool isPlatformMode;
    public bool isSpeedUpMode;
    public bool isFreezingMode;
    public bool isJumpMode;
    public bool isRunnerMode;

    [Header("Stars")]
    [SerializeField] private float _timeTwoStars;
    [SerializeField] private float _timeThreeStars;

    private GameObject _joystickUI;
    private IARManager _IARManager;

    private int _countStars = 1;

    public static float passingTime = 0;

    [NonSerialized] public int lastCompletedNormalLevel;
    [NonSerialized] public int lastCompletedInvisibleLevel;
    [NonSerialized] public int lastCompletedQuessLevel;
    [NonSerialized] public int lastCompletedFlyLevel;
    [NonSerialized] public int lastCompletedPlatformLevel;
    [NonSerialized] public int lastCompletedSpeedUpLevel;
    [NonSerialized] public int lastCompletedFreezingLevel;
    [NonSerialized] public int lastCompletedJumpLevel;
    [NonSerialized] public int lastCompletedRunnerLevel;

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
            if (isNormalMode)
            {
                RewardStars();
                FinishNormalMode();
            }

            else if (isInvisibleMode)
                FinishInvisibleMode();

            else if (isQuessMode)
                FinishQuessMode();

            else if (isFlyMode)
                FinishFlyMode();

            else if (isPlatformMode)
                FinishPlatformMode();

            else if (isSpeedUpMode)
                FinishSpeedUpMode();

            else if (isFreezingMode)
                FinishFreezingMode();

            else if (isJumpMode)
                FinishJumpMode();

            else if (isRunnerMode)
                FinishRunnerMode();

            else
                throw new Exception("None of the game modes are connected to the finish");

            _finishMenu.SetActive(true);

            if (_joystickUI)
                _joystickUI.SetActive(false);

            LevitationBlockSound.isStop = true;
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
            if (!LevelsController.countStarsNormalMode.TryGetValue(CurrentLevel, out _))
            {
                LevelsController.countStarsNormalMode[CurrentLevel] = _countStars;
            }

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

    private void FinishInvisibleMode()
    {
        lastCompletedInvisibleLevel = LevelsController.lastCompletedInvisibleLevel;

        if (LevelsController.lastCompletedInvisibleLevel < CurrentLevel)
            LevelsController.lastCompletedInvisibleLevel = CurrentLevel;

        Progress.Instance.progressData.completedInvisibleLevels = LevelsController.lastCompletedInvisibleLevel;
        Progress.Instance.Save();

        _box.SetActive(true);
    }

    private void FinishQuessMode()
    {
        lastCompletedQuessLevel = LevelsController.lastCompletedQuessLevel;

        if (LevelsController.lastCompletedQuessLevel < CurrentLevel)
            LevelsController.lastCompletedQuessLevel = CurrentLevel;

        Progress.Instance.progressData.completedQuessLevels = LevelsController.lastCompletedQuessLevel;
        Progress.Instance.Save();

        _box.SetActive(true);
    }

    private void FinishFlyMode()
    {
        lastCompletedFlyLevel = LevelsController.lastCompletedFlyLevel;

        if (LevelsController.lastCompletedFlyLevel < CurrentLevel)
            LevelsController.lastCompletedFlyLevel = CurrentLevel;

        Progress.Instance.progressData.completedFlyLevels = LevelsController.lastCompletedFlyLevel;
        Progress.Instance.Save();

        _box.SetActive(true);
    }

    private void FinishPlatformMode()
    {
        lastCompletedPlatformLevel = LevelsController.lastCompletedPlatformLevel;

        if (LevelsController.lastCompletedPlatformLevel < CurrentLevel)
            LevelsController.lastCompletedPlatformLevel = CurrentLevel;

        Progress.Instance.progressData.completedPlatformLevels = LevelsController.lastCompletedPlatformLevel;
        Progress.Instance.Save();

        _box.SetActive(true);
    }

    private void FinishSpeedUpMode()
    {
        lastCompletedSpeedUpLevel = LevelsController.lastCompletedSpeedUpLevel;

        if (LevelsController.lastCompletedSpeedUpLevel < CurrentLevel)
            LevelsController.lastCompletedSpeedUpLevel = CurrentLevel;

        Progress.Instance.progressData.completedSpeedUpLevels = LevelsController.lastCompletedSpeedUpLevel;
        Progress.Instance.Save();

        _box.SetActive(true);
    }

    private void FinishFreezingMode()
    {
        lastCompletedFreezingLevel = LevelsController.lastCompletedFreezingLevel;

        if (LevelsController.lastCompletedFreezingLevel < CurrentLevel)
            LevelsController.lastCompletedFreezingLevel = CurrentLevel;

        Progress.Instance.progressData.completedFreezingLevels = LevelsController.lastCompletedFreezingLevel;
        Progress.Instance.Save();

        _box.SetActive(true);
    }

    private void FinishJumpMode()
    {
        lastCompletedJumpLevel = LevelsController.lastCompletedJumpLevel;

        if (LevelsController.lastCompletedJumpLevel < CurrentLevel)
            LevelsController.lastCompletedJumpLevel = CurrentLevel;

        Progress.Instance.progressData.completedJumpLevels = LevelsController.lastCompletedJumpLevel;
        Progress.Instance.Save();

        _box.SetActive(true);
    }

    private void FinishRunnerMode()
    {
        lastCompletedRunnerLevel = LevelsController.lastCompletedRunnerLevel;

        if (LevelsController.lastCompletedRunnerLevel < CurrentLevel)
            LevelsController.lastCompletedRunnerLevel = CurrentLevel;

        Progress.Instance.progressData.completedRunnerLevels = LevelsController.lastCompletedRunnerLevel;
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
