using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private int _level;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (LevelsController.lastCompletedLevel < _level)
                LevelsController.lastCompletedLevel = _level;

            Time.timeScale = 0;

            _finishMenu.SetActive(true);
        }
    }
}
