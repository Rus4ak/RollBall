using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Loading _loading;

    private string _adUnitId;
    
    [NonSerialized] public bool isAdWatched;

    private void Awake()
    {
        _adUnitId = "Rewarded_Android";
        isAdWatched = false;
    }

    public void LoadRewardedAd()
    {
        _loading.StartLoading();
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (_loading.isLoading)
            ShowRewardedAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowClick(string placementId) {}

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == _adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            isAdWatched = true;
            _loading.StopLoading();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }
}
