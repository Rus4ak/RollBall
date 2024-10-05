using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private int _currentLevel;
    [SerializeField] private GameObject _box;
    [SerializeField] private int _minCountCoins;
    [SerializeField] private int _maxCountCoins;

    [SerializeField] private bool _isDropSkin = false;

    private GameObject _joystickUI;

    public int lastCompletedLevel;

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
            lastCompletedLevel = LevelsController.lastCompletedLevel;

            if (LevelsController.lastCompletedLevel <= CurrentLevel)
                LevelsController.lastCompletedLevel = CurrentLevel + 1;
            
            Progress.Instance.progressData.completedLevels = LevelsController.lastCompletedLevel;
            Progress.Instance.Save();

            _finishMenu.SetActive(true);

            if (_joystickUI)
                _joystickUI.SetActive(false);
            
            _box.SetActive(true);

            collision.gameObject.SetActive(false);
        }
    }
}
