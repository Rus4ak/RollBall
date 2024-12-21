using System;
using UnityEngine;

// Calling the game restart event depending on the selected restart method
public class RestartGame : MonoBehaviour
{
    private RewardedAds _rewardedAds;

    public static event Action<Vector3> PlayerDied;

    private void Awake()
    {
        _rewardedAds = GetComponent<RewardedAds>();
    }

    private void Update()
    {
        if (_rewardedAds.isAdWatched)
        {
            PlayerDied?.Invoke(PlayerMovement.lastPosition);
            _rewardedAds.isAdWatched = false;
        }
    }

    public void RestartToCheckPoint()
    {
        PlayerDied?.Invoke(CheckPoint.checkPoint);
    }

    public void RestartToLastPosition()
    {
        _rewardedAds.LoadRewardedAd();
    }
}
