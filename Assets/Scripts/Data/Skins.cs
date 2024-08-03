using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Skins
{
    private string path = Application.persistentDataPath + "/SkinsData.json";

    public SkinsData skinsData = new SkinsData();

    public static Skins instance
    {
        get
        {
            if (_instance == null)
                _instance = new Skins();

            return _instance;
        }
    }
    
    private static Skins _instance;

    [Serializable]
    public class SkinsData
    {
        public string equippedSkin;
        public List<string> boughtSkins = new List<string>();
    }

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath))
            Directory.CreateDirectory(Application.persistentDataPath);

        File.WriteAllText(path, JsonUtility.ToJson(skinsData));
    }

    public void Load()
    {
        if (!File.Exists(path))
            return;
        
        skinsData = JsonUtility.FromJson<SkinsData>(File.ReadAllText(path));
    }
}
