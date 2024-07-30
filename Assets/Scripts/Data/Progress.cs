using System;
using System.IO;
using UnityEngine;

public class Progress
{
    private string path = Application.streamingAssetsPath + "/ProgressData.json";

    public ProgressData progressData = new ProgressData();
    public static Progress instance
    {
        get
        {
            if (_instance == null)
                _instance = new Progress();

            return _instance;
        }
    }

    private static Progress _instance;

    [Serializable]
    public class ProgressData
    {
        public int bank;
        public int completedLevels;
    }

    public void Save()
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(Application.streamingAssetsPath);

        File.WriteAllText(path, JsonUtility.ToJson(progressData));
    }

    public void Load()
    {
        if (!File.Exists(path))
            return;

        progressData = JsonUtility.FromJson<ProgressData>(File.ReadAllText(path));

        Bank.instance.coins = progressData.bank;
        LevelsController.lastCompletedLevel = progressData.completedLevels;
    }
}
