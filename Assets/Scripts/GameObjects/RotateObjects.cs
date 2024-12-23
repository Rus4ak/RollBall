using System.Collections;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _moveTime;

    [SerializeField] private bool _waitPlayer = false;

    private bool _startRotate;
    private Quaternion _startPositionQuaternion;
    private Quaternion _endPositionQuaternion;

    private float _elapsedTime = 0f;

    private void Start()
    {
        _startRotate = !_waitPlayer;

        _startPositionQuaternion = Quaternion.Euler(_startPosition);
        _endPositionQuaternion = Quaternion.Euler(_endPosition);

        transform.rotation = _startPositionQuaternion;
    }

    private void FixedUpdate()
    {
        if (_startRotate)
            RotateObject();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_waitPlayer)
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(WaitTimeForRotate());
    }

    private void RotateObject()
    {
        // Rotate of an object from point to point in a specified amount of time
        _elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(_elapsedTime / _moveTime);

        transform.rotation = Quaternion.Lerp(_startPositionQuaternion, _endPositionQuaternion, t);

        if (t >= 1.0f)
        {
            if (_waitPlayer)
            {
                _startRotate = false;
                return;
            }

            // Swap the start and end points
            _endPositionQuaternion = _startPositionQuaternion;
            _startPositionQuaternion = transform.rotation;
            _elapsedTime = 0f;
        }
    }

    // Delay before starting to move
    private IEnumerator WaitTimeForRotate()
    {
        yield return new WaitForSeconds(.1f);

        _startRotate = true;

        StopAllCoroutines();
    }
}
