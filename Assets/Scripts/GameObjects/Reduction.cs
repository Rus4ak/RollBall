using UnityEngine;

public class Reduction : MonoBehaviour
{
    [SerializeField] private Vector3 _size;
    [SerializeField] private float _time;

    private Vector3 _startSize;
    private float _elapsedTime;
    private bool _isStart;

    private void Start()
    {
        _startSize = transform.localScale;
        _elapsedTime = 0;
        _isStart = false;
    }

    private void FixedUpdate()
    {
        if (_isStart)
        {
            _elapsedTime += Time.fixedDeltaTime / _time;
            transform.localScale = Vector3.Lerp(_startSize, _size, _elapsedTime);

            if (_elapsedTime >= 1f)
                _isStart = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _isStart = true;
    }
}
