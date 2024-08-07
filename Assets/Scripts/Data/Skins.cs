using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting.FullSerializer;

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
        public Dictionary<string, string> equippedSkins = new Dictionary<string, string>();
        public List<string> boughtSkins = new List<string>();
    }

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath))
            Directory.CreateDirectory(Application.persistentDataPath);

        File.WriteAllText(path, JsonConvert.SerializeObject(skinsData));
    }

    public void Load()
    {
        if (!File.Exists(path))
            return;
        
        skinsData = JsonConvert.DeserializeObject<SkinsData>(File.ReadAllText(path));
    }
}
