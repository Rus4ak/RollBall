using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class FinishMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _passageTimeTMP;
    [SerializeField] private TextMeshProUGUI _bestTimeTMP;
    [SerializeField] private GameObject _stars;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private TextMeshProUGUI _twoStarsTMP;
    [SerializeField] private TextMeshProUGUI _threeStarsTMP;
    [SerializeField] private List<Button> _buttons;

    private int _bestTime;
    private Finish _finish;
    private Star _star;
    private int _currentStar;

    private bool _isEndStars = false;

    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        
        foreach (Button button in _buttons) 
            button.interactable = false;

        PassageTimeInitialize();
        StarsInitialize();
    }

    private void Update()
    {
        if (_currentStar + 1 > _finish.CountStars)
        {
            if (!_isEndStars)
            {
                _finish.Box.SetActive(true);
                
                foreach (Button button in _buttons)
                    button.interactable = true;
                
                _isEndStars = true;
            }

            return;
        }

        if (_star == null)
        {
            _star = Instantiate(_starPrefab, transform).GetComponent<Star>();
            _star.SetEndPosition(_stars.transform.GetChild(_currentStar).transform.position);
        }

        if (_star.isStop)
        {
            _star = null;
            _currentStar += 1;
        }
    }

    private void PassageTimeInitialize()
    {
        if (!BestPassedTime.Instance.BestTime.TryGetValue(_finish.CurrentLevel, out _bestTime))
        {
            _bestTime = (int)Finish.passingTime;
            BestPassedTime.Instance.BestTime[_finish.CurrentLevel] = _bestTime;
            BestPassedTime.Instance.SaveData();
        }

        if (_bestTime > (int)Finish.passingTime)
        {
            _bestTime = (int)Finish.passingTime;
            BestPassedTime.Instance.BestTime[_finish.CurrentLevel] = _bestTime;
            BestPassedTime.Instance.SaveData();
        }

        _passageTimeTMP.text += $" {(int)Finish.passingTime} {LocalizationSettings.StringDatabase.GetLocalizedString("StringTable", "sec")}";
        _bestTimeTMP.text += $" {_bestTime} {LocalizationSettings.StringDatabase.GetLocalizedString("StringTable", "sec")}";
    }

    private void StarsInitialize()
    {
        _twoStarsTMP.text += $" {_finish.TimeTwoStars} {LocalizationSettings.StringDatabase.GetLocalizedString("StringTable", "sec")}";
        _threeStarsTMP.text += $" {_finish.TimeThreeStars} {LocalizationSettings.StringDatabase.GetLocalizedString("StringTable", "sec")}";
    }
}
