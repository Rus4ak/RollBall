using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialTip : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tipObjects;
    [SerializeField] private GameObject _tipUI;
    [SerializeField] private TypeWriterAnimation _tipText;

    private GameObject _joystick;
    private GameObject _pause;

    private bool _showTip = true;
    private bool _isTipActive = false;

    private void Start()
    {
        _joystick = GameObject.FindWithTag("Joystick");
        _pause = FindAnyObjectByType<PauseGame>().gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_showTip)
            {
                // Activating the tip
                _tipUI.SetActive(true);

                foreach (GameObject tipObject in _tipObjects) 
                    tipObject.layer = 11;

                // Stopping the player movement
                PlayerMovement._isStop = true;
                
                // Disabling the joystick
                _joystick.SetActive(false);

                _isTipActive = true;
                _showTip = false;

                // Disabling the pause button
                _pause.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (_isTipActive)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (_tipText.isAnimationEnd)
                {
                    // Disabling the tip
                    _tipUI.SetActive(false);

                    foreach (GameObject tipObject in _tipObjects)
                        tipObject.layer = 0;

                    PlayerMovement._isStop = false;
                    _joystick.SetActive(true);

                    // Returning the joystick to its initial state
                    _joystick.transform.GetChild(0).GetComponent<FloatingJoystick>().OnPointerUp(new PointerEventData(EventSystem.current));

                    _isTipActive = false;

                    // Activating the pause button
                    _pause.SetActive(true);
                }
            }
        }
    }
}
