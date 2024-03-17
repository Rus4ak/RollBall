using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    
    [SerializeField] private float _speed = 11f;
    [SerializeField] private float _maxSpeed = 14f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;

            _finishMenu.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            Vector3 movement = touchDeltaPosition.x * cameraRight + touchDeltaPosition.y * cameraForward;
            movement.y = 0;
            
            _rigidbody.AddForce(movement * _speed * Time.fixedDeltaTime);

            Vector3 velocity = _rigidbody.velocity;
            float velocityY = velocity.y;

            velocity.y = 0;

            if (velocity.magnitude > _maxSpeed)
            {
                velocity = _rigidbody.velocity.normalized * _maxSpeed;
                velocity.y = velocityY;

                _rigidbody.velocity = velocity;
            }
        }
    }
}
