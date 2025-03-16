using System.Collections;
using UnityEngine;

public class FullRotate : MonoBehaviour
{
    [SerializeField, Range(-1f, 1f)] private int _directionX;
    [SerializeField, Range(-1f, 1f)] private int _directionY;
    [SerializeField, Range(-1f, 1f)] private int _directionZ;
    [SerializeField] private float _timeRotate;

    [SerializeField] private bool _waitPlayer = false;
    [SerializeField] private float _pauseTime = .1f;

    private Vector3 _direction;
    private float _rotateSpeed;
    private bool _startRotate;

    private void Start()
    {
        _direction = new Vector3(_directionX, _directionY, _directionZ);
        _rotateSpeed = 360f / _timeRotate;
        _startRotate = !_waitPlayer;
    }

    private void FixedUpdate()
    {
        if (_startRotate)
        {
            transform.Rotate(_rotateSpeed * Time.deltaTime * _direction);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_waitPlayer)
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(WaitTimeForRotate());
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_waitPlayer)
            if (collision.gameObject.CompareTag("Player"))
                _startRotate = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_waitPlayer)
            if (other.gameObject.CompareTag("Player"))
                StartCoroutine(WaitTimeForRotate());
    }

    private void OnTriggerExit(Collider other)
    {
        if (_waitPlayer)
            if (other.gameObject.CompareTag("Player"))
                _startRotate = false;
    }

    // Delay before starting to move
    private IEnumerator WaitTimeForRotate()
    {
        yield return new WaitForSeconds(_pauseTime);

        _startRotate = true;

        StopAllCoroutines();
    }
}
