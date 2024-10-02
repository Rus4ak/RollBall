using System;
using TMPro;
using UnityEngine;

public class TypeWriterAnimation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _keyboardSound;
    
    private TextMeshProUGUI _text;
    private string _textTemp;
    private float _elapsedTime;

    [NonSerialized] public bool isAnimationEnd = false;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textTemp = _text.text;
       
        _text.text = "";
    }

    private void Update()
    {
        // Skip the animation
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!isAnimationEnd)
            {
                _text.text += _textTemp;
                _textTemp = null;
                isAnimationEnd = true;
            }
        }

        _elapsedTime += Time.deltaTime;

        // At a specified time interval, the first letter from _textTemp
        // is transferred to _text and removed from _textTemp
        if (_elapsedTime > _speed)
        {
            if (_textTemp != null)
            {
                _text.text += _textTemp[0];

                if (!_keyboardSound.isPlaying)
                {
                    _keyboardSound.pitch = UnityEngine.Random.Range(.9f, 1.1f);
                    _keyboardSound.Play();
                }
                    //_keyboardSound.Stop();


                if (_textTemp.Length > 1)
                    _textTemp = _textTemp.Substring(1);
                else if (_textTemp.Length == 1)
                {
                    _textTemp = null;
                    isAnimationEnd = true;
                }

                _elapsedTime = 0;
            }
        }
    }
}
