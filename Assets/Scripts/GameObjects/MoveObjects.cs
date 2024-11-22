using System.Collections;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _pauseTime = .5f;
    [SerializeField] private bool _waitPlayer = false;

    private bool _startMove;
    
    private float _elapsedTime = 0f;

    private void Start()
    {
        transform.position = _startPosition;
        _startMove = !_waitPlayer;
    }

    private void FixedUpdate()
    {
        if (_startMove) 
            MoveObject();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_waitPlayer)
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(WaitTimeForMove());
    }

    private void MoveObject()
    {
        // Movement of an object from point to point in a specified amount of time
        _elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(_elapsedTime / _moveTime);

        transform.position = Vector3.Lerp(_startPosition, _endPosition, t);
        
        if (t >= 1.0f)
        {
            if (_waitPlayer)
                return;

            StartCoroutine(WaitForMove());
        }
    }

    // Delay before starting to move and swap the start and end points
    private IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(_pauseTime);

        _endPosition = _startPosition;
        _startPosition = transform.position;
        _elapsedTime = 0f;

        StopAllCoroutines();
    }

    // Delay before starting to move
    private IEnumerator WaitTimeForMove()
    {
        yield return new WaitForSeconds(.1f);

        _startMove = true;

        StopAllCoroutines();
    }
}
