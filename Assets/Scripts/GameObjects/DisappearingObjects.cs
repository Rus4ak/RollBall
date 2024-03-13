using UnityEngine;

public class DisappearingObjects : MonoBehaviour
{
    [SerializeField] private float _disaperingTime;

    private Material _material;
    private Color _color = new Color(.2f, 0, 0, 1f);
    private float _elapsedTime = 0f;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.transform.CompareTag("Player"))
        {
            _elapsedTime += Time.deltaTime / _disaperingTime;
            _material.color = Color.Lerp(_material.color, _color, _elapsedTime);
            
            if (_elapsedTime >= 1f)
                Destroy(gameObject);
        }
    }
}
