using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _moveTime;

    private float _elapsedTime = 0f;
    private Quaternion _startPositionQuaternion;
    private Quaternion _endPositionQuaternion;

    private void Start()
    {
        _startPositionQuaternion = Quaternion.Euler(_startPosition);
        _endPositionQuaternion = Quaternion.Euler(_endPosition);

        transform.rotation = _startPositionQuaternion;
    }

    private void FixedUpdate()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        _elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(_elapsedTime / _moveTime);

        transform.rotation = Quaternion.Lerp(_startPositionQuaternion, _endPositionQuaternion, t);

        if (t >= 1.0f)
        {
            _endPositionQuaternion = _startPositionQuaternion;
            _startPositionQuaternion = transform.rotation;
            _elapsedTime = 0f;
        }
    }
}
