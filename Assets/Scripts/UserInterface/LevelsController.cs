using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Level
{
    public Button button;
    public TextMeshProUGUI text;
    public Image locked;
    public GameObject stars;

    public Level(Button button, TextMeshProUGUI text, Image locked, GameObject stars)
    {
        this.button = button;
        this.text = text;
        this.locked = locked;
        this.stars = stars;
    }
}

public class LevelsController : MonoBehaviour
{
    [SerializeField] private List<Level> _normalLevels;
    [SerializeField] private List<Level> _invisibleLevels;
    [SerializeField] private List<Level> _quessLevels;
    [SerializeField] private List<Level> _flyLevels;
    [SerializeField] private List<Level> _platformLevels;
    [SerializeField] private List<Level> _speedUpLevels;
    [SerializeField] private List<Level> _freezingLevels;
    [SerializeField] private List<Level> _jumpLevels;
    [SerializeField] private List<Level> _runnerLevels;

    private bool _isSaveData = false;

    public static int lastCompletedNormalLevel = 0;
    public static int lastCompletedInvisibleLevel = 0;
    public static int lastCompletedQuessLevel = 0;
    public static int lastCompletedFlyLevel = 0;
    public static int lastCompletedPlatformLevel = 0;
    public static int lastCompletedSpeedUpLevel = 0;
    public static int lastCompletedFreezingLevel = 0;
    public static int lastCompletedJumpLevel = 0;
    public static int lastCompletedRunnerLevel = 0;

    public static Dictionary<int, int> countStarsNormalMode = new Dictionary<int, int>();

    private void Start()
    {
        InitializeNormalLevels();
        InitializeMiniGameLevels(lastCompletedInvisibleLevel, _invisibleLevels);
        InitializeMiniGameLevels(lastCompletedQuessLevel, _quessLevels);
        InitializeMiniGameLevels(lastCompletedFlyLevel, _flyLevels);
        InitializeMiniGameLevels(lastCompletedPlatformLevel, _platformLevels);
        InitializeMiniGameLevels(lastCompletedSpeedUpLevel, _speedUpLevels);
        InitializeMiniGameLevels(lastCompletedFreezingLevel, _freezingLevels);
        InitializeMiniGameLevels(lastCompletedJumpLevel, _jumpLevels);
        InitializeMiniGameLevels(lastCompletedRunnerLevel, _runnerLevels);

        if (_isSaveData)
            Progress.Instance.Save();

        if (countStarsNormalMode.Count > _normalLevels.Count)
        {
            if (countStarsNormalMode.TryGetValue(0, out _))
            {
                countStarsNormalMode.Remove(0);
            }
        }

        if (countStarsNormalMode.Count < _normalLevels.Count)
        {
            for (int i = countStarsNormalMode.Count; i < _normalLevels.Count; i++)
            {
                countStarsNormalMode[i + 1] = 0;
            }
        }

        if (Progress.Instance.progressData.countStarsNormalMode.Count < _normalLevels.Count)
        {
            for (int i = Progress.Instance.progressData.countStarsNormalMode.Count; i < _normalLevels.Count; i++)
            {
                Progress.Instance.progressData.countStarsNormalMode[i + 1] = 0;
                Progress.Instance.Save();
            }
        }
    }

    private void InitializeNormalLevels()
    {
        if (lastCompletedNormalLevel >= _normalLevels.Count)
            lastCompletedNormalLevel = _normalLevels.Count;

        // Activating the buttons for the completed level and the next level
        for (int i = 0; i < lastCompletedNormalLevel + 1; i++)
        {
            if (i >= _normalLevels.Count)
                return;

            Level level = _normalLevels[i];
            level.button.interactable = true;
            level.text.gameObject.SetActive(true);
            level.locked.gameObject.SetActive(false);
            level.stars.gameObject.SetActive(true);

            int starsCount = 0;

            if (i + 1 <= lastCompletedNormalLevel)
            {
                if (!countStarsNormalMode.TryGetValue(i + 1, out starsCount))
                {
                    countStarsNormalMode[i] = 1;
                    starsCount = 1;

                    if (Progress.Instance.progressData.countStarsNormalMode == null)
                        Progress.Instance.progressData.countStarsNormalMode = new Dictionary<int, int>();

                    Progress.Instance.progressData.countStarsNormalMode[i] = 1;
                    _isSaveData = true;
                }
            }
            
            for (int j = 0; j < starsCount; j++)
            {
                level.stars.transform.GetChild(j).GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void InitializeMiniGameLevels(int lastCompletedLevel, List<Level> levels)
    {
        if (lastCompletedLevel >= levels.Count)
            lastCompletedLevel = levels.Count;

        // Activating the buttons for the completed level and the next level
        for (int i = 0; i < lastCompletedLevel + 1; i++)
        {
            if (i >= levels.Count)
                return;

            Level level = levels[i];
            level.button.interactable = true;
            level.text.gameObject.SetActive(true);
            level.locked.gameObject.SetActive(false);
        }
    }
}
