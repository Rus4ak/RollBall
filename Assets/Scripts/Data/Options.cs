using System;
using System.IO;
using UnityEngine;

public class Options
{
    private readonly string _path = Application.persistentDataPath + "/OptionsData.json";

    public OptionsData optionsData = new OptionsData();

    private static Options _instance;

    // Implementation of the Singleton pattern
    public static Options Instance
    {
        get
        {
            _instance ??= new Options();

            return _instance;
        }
    }

    // Class with fields for storage
    [Serializable]
    public class OptionsData
    {
        public int quality;
        public float musicVolume;
        public float soundVolume;
    }

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath))
            Directory.CreateDirectory(Application.persistentDataPath);

        File.WriteAllText(_path, JsonUtility.ToJson(optionsData));
    }

    public void Load()
    {
        if (!File.Exists(_path))
            return;
        
        optionsData = JsonUtility.FromJson<OptionsData>(File.ReadAllText(_path));

        Quality.quality = optionsData.quality;
        MusicVolume.volume = optionsData.musicVolume;
        SoundVolume.volume = optionsData.soundVolume;
    }
}
