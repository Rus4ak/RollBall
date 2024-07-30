using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private int _level;
    [SerializeField] private GameObject _box;
    [SerializeField] private int _minCoutCoins;
    [SerializeField] private int _maxCoutCoins;

    public static int countCoins;

    private void Start()
    {
        countCoins = Random.Range(_minCoutCoins, _maxCoutCoins);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (LevelsController.lastCompletedLevel < _level)
            {
                LevelsController.lastCompletedLevel = _level;
                Progress.instance.progressData.completedLevels = _level;
                Progress.instance.Save();
            }

            _finishMenu.SetActive(true);

            Instantiate(_box);

            collision.gameObject.SetActive(false);
        }
    }
}
