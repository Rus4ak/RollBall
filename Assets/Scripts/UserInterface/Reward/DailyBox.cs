using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBox : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] TextMeshProUGUI _timeText;
    [SerializeField] Image _imageBox;
    [SerializeField] GameObject _adsImage;

    private Button _button;
    private DateTime _currentTime;
    private DateTime _lastOpenTime;
    private TimeSpan _timeLeft;
    private bool _isAvailable;
    private RewardedAds _rewardedAds;

    private void Start()
    {
        _button = GetComponent<Button>();
        _lastOpenTime = Progress.Instance.progressData.lastOpenDailyBoxTime;
        _rewardedAds = GetComponent<RewardedAds>();

        // If the database does not contain data on the last time the box was opened, the box becomes available
        if (_lastOpenTime == DateTime.MinValue)
            _isAvailable = true;
    }

    private void Update()
    {
        _currentTime = DateTime.Now;

        if (_isAvailable)
            // If the box was not available before, it becomes available
            if (!_button.interactable)
            {
                _button.interactable = true;
                _timeText.gameObject.SetActive(false);
                _imageBox.color = Color.white;
                _adsImage.SetActive(true);
            }

        if (!_isAvailable)
        {
            // If the box was available before, it becomes not available
            if (_button.interactable)
            {
                _button.interactable = false;
                _timeText.gameObject.SetActive(true);
                _imageBox.color = new Color(1, 1, 1, .5f);
                _adsImage.SetActive(false);
            }

            _timeLeft = TimeSpan.FromHours(24) - (_currentTime - _lastOpenTime);
            _timeText.text = $"{_timeLeft.Hours}:{_timeLeft.Minutes}:{_timeLeft.Seconds}";
            
            if (_timeLeft.Hours <= 0)
                _isAvailable = true;
        }

        if (_rewardedAds.isAdWatched)
        {
            _box.SetActive(true);
            _lastOpenTime = _currentTime;
            _isAvailable = false;
            _rewardedAds.isAdWatched = false;

            Progress.Instance.progressData.lastOpenDailyBoxTime = _currentTime;
            Progress.Instance.Save();
        }
    }

    public void OpenBox()
    {
        if (_isAvailable)
            _rewardedAds.LoadRewardedAd();
    }
}
