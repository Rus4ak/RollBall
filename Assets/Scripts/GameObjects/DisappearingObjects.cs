using UnityEngine;

public class DisappearingObjects : MonoBehaviour
{
    [SerializeField] private float _disaperingTime;

    private Material _material;
    private Color _defaultColor;

    private Color _color = new Color(.2f, 0, 0, 1f);
    private float _elapsedTime = 0f;
    private bool _isStart = false;
    private PlayerMovement _player = null;


    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _defaultColor = _material.color;
    }

    private void Update()
    {
        if (_isStart)
        {
            // Changing the color of the material to red over a specified time
            _elapsedTime += Time.deltaTime / _disaperingTime;
            _material.color = Color.Lerp(_defaultColor, _color, _elapsedTime);

            // Destroying the object after a certain time has passed
            if (_elapsedTime >= 1f)
                if (gameObject.transform.parent != null && gameObject.transform.parent.name != "Map")
                    Destroy(gameObject.transform.parent.gameObject);
                else
                    Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out _player))
            _isStart = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out _player))
            _player = null;
    }

    private void OnDestroy()
    {
        if (_player)
            _player.isCollision = false;
    }
}
