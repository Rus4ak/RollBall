using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _posZForStart;
    [SerializeField] private Vector3 _posToMove;
    [SerializeField] private float _timeMove;
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private float _wheelSpeed;

    private bool _isStart = false;

    private void Update()
    {
        if (!_isStart)
            if (_player.position.z >= _posZForStart)
            {
                _isStart = true;
                StartCoroutine(Move());
            }
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;

        while (elapsedTime < _timeMove)
        {
            transform.position = Vector3.Lerp(startPos, _posToMove, elapsedTime / _timeMove);
            
            foreach (Transform wheel in _wheels)
            {
                Vector3 rotation = wheel.localRotation.eulerAngles;
                rotation.z -= _wheelSpeed;
                wheel.localRotation = Quaternion.Lerp(wheel.localRotation, Quaternion.Euler(rotation), _wheelSpeed * Time.deltaTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StopCoroutine(Move());
    }
}
