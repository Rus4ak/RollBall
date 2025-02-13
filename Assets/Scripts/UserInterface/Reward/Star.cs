using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject _starImage;
    [SerializeField] private AudioSource _starStopSound;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _elapsedTime;
    
    public bool isStop;

    public void SetEndPosition(Vector3 endPosition)
    {
        _endPosition = endPosition;
    }

    private void Start()
    {
        _startPosition = _starImage.transform.position;
    }

    private void Update()
    {
        if (isStop)
            return;

        _elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(_elapsedTime / 1.18f);

        _starImage.transform.position = Vector3.Lerp(_startPosition, _endPosition, t);

        if (t >= 1f)
        {
            isStop = true;
            _starStopSound.Play();
        }
    }
}
