using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

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
        public Dictionary<string, string> equippedSkins = new Dictionary<string, string>()
        {
            { "ball", null },
            { "block", null },
            { "background", null }
        };
        public List<string> boughtSkins = new List<string>();
    }

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath))
            Directory.CreateDirectory(Application.persistentDataPath);

        byte[] data = SerializeToBytes(skinsData);

        File.WriteAllBytes(path, data);

    }

    public void Load()
    {
        if (!File.Exists(path))
            return;

        byte[] bytes = File.ReadAllBytes(path);
        skinsData = DeserializeFromBytes(bytes);
    }

    private byte[] SerializeToBytes(SkinsData data)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, data);
            return memoryStream.ToArray();
        }
    }

    private SkinsData DeserializeFromBytes(byte[] bytes)
    {
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (SkinsData)formatter.Deserialize(memoryStream);
        }
    }
}
