using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Progress
{
    private readonly string _path = Application.persistentDataPath + "/ProgressData.json";

    public ProgressData progressData = new ProgressData();
    
    private static Progress _instance;

    // Implementation of the Singleton pattern
    public static Progress Instance
    {
        get
        {
            _instance ??= new Progress();

            return _instance;
        }
    }

    // Class with fields for storage
    [Serializable]
    public class ProgressData
    {
        public int bank;
        public int completedNormalLevels;
        public int completedInvisibleLevels;
        public int completedQuessLevels;
        public int completedFlyLevels;
        public int completedPlatformLevels;
        public int completedSpeedUpLevels;
        public int completedFreezingLevels;
        public int completedJumpLevels;
        public int completedRunnerLevels;
        public Dictionary<int, int> countStarsNormalMode = new Dictionary<int, int>();
        public Dictionary<int, int> bestPassedTime = new Dictionary<int, int>();
        public DateTime lastOpenDailyBoxTime;
        public bool isShownReview = false;
    }

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath))
            Directory.CreateDirectory(Application.persistentDataPath);

        // Serialize data to bytes
        byte[] data = SerializeToBytes(progressData);

        File.WriteAllBytes(_path, data);
    }

    public void Load()
    {
        if (!File.Exists(_path))
            return;

        // Deserialize data from bytes
        byte[] bytes = File.ReadAllBytes(_path);
        progressData = DeserializeFromBytes(bytes);

        Bank.Instance.Coins = progressData.bank;
        LevelsController.lastCompletedNormalLevel = progressData.completedNormalLevels;
        LevelsController.lastCompletedInvisibleLevel = progressData.completedInvisibleLevels;
        LevelsController.lastCompletedQuessLevel = progressData.completedQuessLevels;
        LevelsController.lastCompletedFlyLevel = progressData.completedFlyLevels;
        LevelsController.lastCompletedPlatformLevel = progressData.completedPlatformLevels;
        LevelsController.lastCompletedSpeedUpLevel = progressData.completedSpeedUpLevels;
        LevelsController.lastCompletedFreezingLevel = progressData.completedFreezingLevels;
        LevelsController.lastCompletedJumpLevel = progressData.completedJumpLevels;
        LevelsController.lastCompletedRunnerLevel = progressData.completedRunnerLevels;

        if (progressData.countStarsNormalMode != null)
            LevelsController.countStarsNormalMode = progressData.countStarsNormalMode;
        
        if (progressData.bestPassedTime != null)
            BestPassedTime.Instance.BestTime = progressData.bestPassedTime;
        
        IARManager.isShownReview = progressData.isShownReview;
    }

    private byte[] SerializeToBytes(ProgressData data)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, data);
            return memoryStream.ToArray();
        }
    }

    private ProgressData DeserializeFromBytes(byte[] bytes)
    {
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (ProgressData)formatter.Deserialize(memoryStream);
        }
    }
}
