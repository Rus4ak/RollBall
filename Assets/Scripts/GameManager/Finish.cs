using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private int _level;
    [SerializeField] private GameObject _box;
    [SerializeField] private int _minCoutCoins;
    [SerializeField] private int _maxCoutCoins;

    private GameObject _joystickUI;

    public static int countCoins;

    private void Start()
    {
        countCoins = Random.Range(_minCoutCoins, _maxCoutCoins);
        _joystickUI = GameObject.FindGameObjectWithTag("Joystick");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (LevelsController.lastCompletedLevel < _level)
                LevelsController.lastCompletedLevel = _level;
            
            Progress.instance.progressData.completedLevels = LevelsController.lastCompletedLevel;
            Progress.instance.Save();

            _finishMenu.SetActive(true);

            if (_joystickUI)
                _joystickUI.SetActive(false);
            
            _box.SetActive(true);

            collision.gameObject.SetActive(false);
        }
    }
}
