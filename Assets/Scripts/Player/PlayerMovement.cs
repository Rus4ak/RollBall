using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 11f;
    [SerializeField] private float _maxSpeed = 14f;
    [SerializeField] private GameObject _restartMenu;

    private Rigidbody _rigidbody;

    public static Vector3 lastPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        
        if (transform.position.y < -10f)
        {
            _restartMenu.SetActive(true);

            Time.timeScale = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10)
            return;

        lastPosition = collision.gameObject.transform.position;
        lastPosition.y += 1f;
    }

    private void OnEnable()
    {
        RestartGame.PlayerDied += Respawn;
    }

    private void OnDestroy()
    {
        RestartGame.PlayerDied -= Respawn;
    }

    public void Respawn(Vector3 position)
    {
        Time.timeScale = 1;
        
        _rigidbody.Sleep();
    
        transform.position = position;
        
        Transform camera = Camera.main.transform;
        Vector3 cameraPosition = transform.position;

        cameraPosition.y += 5f;
        cameraPosition.z -= 7f;

        camera.position = cameraPosition;

        _restartMenu.SetActive(false);
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
