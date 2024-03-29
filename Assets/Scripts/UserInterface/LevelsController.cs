using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    public static int lastCompletedLevel = 0;

    private void Start()
    {
        for (int i = 0; i < lastCompletedLevel + 1; i++)
        {
            if (i >= _levels.Count)
                return;

            Level level = _levels[i];
            level.button.interactable = true;
            level.text.gameObject.SetActive(true);
            level.locked.gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class Level
{
    public Button button;
    public TextMeshProUGUI text;
    public Image locked;

    public Level(Button button, TextMeshProUGUI text, Image locked)
    {
        this.button = button;
        this.text = text;
        this.locked = locked;
    }
}
