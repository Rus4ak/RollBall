using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Skins
{
    private readonly string _path = Application.persistentDataPath + "/SkinsData.json";

    public SkinsData skinsData = new SkinsData();

    private static Skins _instance;

    // Implementation of the Singleton pattern
    public static Skins Instance
    {
        get
        {
            _instance ??= new Skins();

            return _instance;
        }
    }

    // Class with fields for storage
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

        // Serialize data to bytes
        byte[] data = SerializeToBytes(skinsData);

        File.WriteAllBytes(_path, data);

    }

    public void Load()
    {
        if (!File.Exists(_path))
            return;

        // Deserialize data from bytes
        byte[] bytes = File.ReadAllBytes(_path);
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
