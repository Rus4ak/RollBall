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

    // Movement of coins to the specified position, after which they are destroyed
    private void Update()
    {
        _elapsedTime += Time.deltaTime * _speed;

        transform.position = Vector3.Lerp(_startCoinPosition, _moveToPos, _elapsedTime);

        if (Vector3.Distance(transform.position, _moveToPos) < 1f)
            Destroy(gameObject);
    }
}
