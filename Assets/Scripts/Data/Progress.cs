using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Progress
{
    private string path = Application.persistentDataPath + "/ProgressData.json";

    public ProgressData progressData = new ProgressData();
    
    private static Progress _instance;

    // Implementation of the Singleton pattern
    public static Progress instance
    {
        get
        {
            if (_instance == null)
                _instance = new Progress();

            return _instance;
        }
    }

    // Class with fields for storage
    [Serializable]
    public class ProgressData
    {
        public int bank;
        public int completedLevels;
    }

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath))
            Directory.CreateDirectory(Application.persistentDataPath);

        // Serialize data to bytes
        byte[] data = SerializeToBytes(progressData);

        File.WriteAllBytes(path, data);
    }

    public void Load()
    {
        if (!File.Exists(path))
            return;

        // Deserialize data from bytes
        byte[] bytes = File.ReadAllBytes(path);
        progressData = DeserializeFromBytes(bytes);

        Bank.instance.coins = progressData.bank;
        LevelsController.lastCompletedLevel = progressData.completedLevels;
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
