using UnityEngine;

public class DisappearingObjects : MonoBehaviour
{
    [SerializeField] private float _disaperingTime;

    private Material _material;
    private Color _defaultColor;
    private Color _color = new Color(.2f, 0, 0, 1f);
    private float _elapsedTime = 0f;
    private bool _isStart = false;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _defaultColor = _material.color;
    }

    private void Update()
    {
        if (_isStart)
        {
            _elapsedTime += Time.deltaTime / _disaperingTime;
            _material.color = Color.Lerp(_defaultColor, _color, _elapsedTime);

            if (_elapsedTime >= 1f)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.CompareTag("Player"))
            _isStart = true;
    }
}
