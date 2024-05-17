using UnityEngine;

public class TakeRewardAnimation : MonoBehaviour
{
    private Vector3 _moveToPos;
    private Vector3 _startCoinPosition;
    private float _elapsedTime;
    private float _speed;


    private void Start()
    {
        _moveToPos = GameObject.Find("MoveToPos").transform.position;
        _startCoinPosition = transform.position;
        _speed = Random.Range(1f, 3f);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime * _speed;

        transform.position = Vector3.Lerp(_startCoinPosition, _moveToPos, _elapsedTime);

        if (transform.position == _moveToPos)
            Destroy(gameObject);
    }
}
