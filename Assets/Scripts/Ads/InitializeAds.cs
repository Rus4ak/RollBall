using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    private string _gameId;
    private bool _isTesting;

    private class GameConfig
    {
        public string gameId;
    }

    private async void Awake()
    {
        string configPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Config.json");
        string jsonData = null;

        if (configPath.Contains("://") || configPath.Contains(":///"))
        {
            // Extracting gameID on the Android system
            UnityWebRequest request = UnityWebRequest.Get(configPath);
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                jsonData = request.downloadHandler.text;
            }
        }
        else
        {
            // Extracting gameID on other platforms
            if (System.IO.File.Exists(configPath))
                jsonData = System.IO.File.ReadAllText(configPath);
        }

        if (!string.IsNullOrEmpty(jsonData))
        {
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
