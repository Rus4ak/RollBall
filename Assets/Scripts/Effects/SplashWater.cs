using UnityEngine;

public class SplashWater : MonoBehaviour
{
    [SerializeField] private GameObject _splashParticle;

    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = _splashParticle.GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 position = collision.transform.position;
            position.y = 0;
            _splashParticle.transform.position = position;
            _particleSystem.Play();
        }
    }
}
