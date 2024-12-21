using System.IO;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    private string _gameId;
    private bool _isTesting;

    private class GameConfig
    {
        public string gameId;
    }

    private void Awake()
    {
        string configPath = Path.Combine(Application.streamingAssetsPath, "Config.json");

        if (File.Exists(configPath))
        {
            string jsonData = File.ReadAllText(configPath);
            GameConfig config = JsonUtility.FromJson<GameConfig>(jsonData);
            _gameId = config.gameId;
        }

        _isTesting = true;

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _isTesting, this);
        }
    }

    public void OnInitializationComplete() { }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }
}
