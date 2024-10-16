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

    public Level(Button button, TextMeshProUGUI text, Image locked)
    {
        this.button = button;
        this.text = text;
        this.locked = locked;
    }
}

public class LevelsController : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    public static int lastCompletedLevel = 15;

    private void Start()
    {
        if (lastCompletedLevel > _levels.Count)
            lastCompletedLevel = _levels.Count;

        // Activating the buttons for the completed level and the next level
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
