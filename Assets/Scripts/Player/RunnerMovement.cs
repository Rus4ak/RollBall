using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private HoldButtonHandler _leftButton;
    [SerializeField] private HoldButtonHandler _rightButton;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.forward * _speed * Time.fixedDeltaTime);

        if (_leftButton.isHolding)
            _rigidbody.AddForce(Vector3.left * _speed * Time.fixedDeltaTime);

        if (_rightButton.isHolding)
            _rigidbody.AddForce(Vector3.right * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dead"))
            PlayerMovement.isDead = true;

        if (collision.gameObject.CompareTag("Water"))
            _rigidbody.velocity /= 2;
    }
}
