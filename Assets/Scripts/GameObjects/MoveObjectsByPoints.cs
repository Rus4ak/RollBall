using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// A class with position and time properties for the object to reach this position
[System.Serializable]
public class PointData : ICloneable
{
    public Vector3 position;
    public float moveTime;

    public PointData(Vector3 pos, float moveT)
    {
        position = pos;
        moveTime = moveT;
    }

    public object Clone()
    {
        return new PointData(position, moveTime);
    }
}

public class MoveObjectsByPoints : MonoBehaviour
{
    [SerializeField] private List<PointData> _points;
    [SerializeField] private bool _waitPlayer = false;

    private List<PointData> _tempPoints;
    private bool _startMove;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private float _elapsedTime = 0f;

    private void Start()
    {
        // Copying properties from _points to _tempPoints
        _tempPoints = new List<PointData>(_points.Select(p => (PointData)p.Clone()));
        _tempPoints[0].moveTime = 0f;

        _startMove = !_waitPlayer;
        _startPosition = transform.position;
        _endPosition = _tempPoints[0].position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_waitPlayer)
            if (collision.gameObject.CompareTag("Player"))
                _startMove = true;
    }

    private void FixedUpdate()
    {
        if (_startMove)
            Move();
    }

    private void Move()
    {
        // Movement of an object from point to point in a specified amount of time
        PointData point = _tempPoints[0];

        _elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(_elapsedTime / point.moveTime);

        transform.position = Vector3.Lerp(_startPosition, _endPosition, t);

        if (t >= 1f)
        {
            // Deletion of the point to which the object has moved
            _tempPoints.RemoveAt(0);

            // If the object reaches the end point, all points are added to _tempPoints in reverse order
            if (_tempPoints.Count == 0)
            {
                _tempPoints.AddRange(_points);
                
                if (_endPosition != _tempPoints[0].position)
                    _tempPoints.Reverse();

                return;
            }

            _startPosition = transform.position;
            _endPosition = _tempPoints[0].position;
            _elapsedTime = 0f;
        }
    }
}
