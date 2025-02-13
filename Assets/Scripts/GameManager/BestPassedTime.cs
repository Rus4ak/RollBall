using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BestPassedTime
{
    private static BestPassedTime _instance;

    // Implementation of the Singleton pattern
    public static BestPassedTime Instance
    {
        get
        {
            _instance ??= new BestPassedTime();

            return _instance;
        }
    }

    public Dictionary<int, int> BestTime { get; set; }

    private BestPassedTime()
    {
        BestTime = new Dictionary<int, int>();
    }

    public void SaveData()
    {
        Progress.Instance.progressData.bestPassedTime = BestTime;
        Progress.Instance.Save();
    }
}
