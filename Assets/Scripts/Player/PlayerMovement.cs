using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 11f;
    [SerializeField] private float _maxSpeed = 14f;
    [SerializeField] private GameObject _restartMenu;

    private Rigidbody _rigidbody;
    private Renderer _skin;

    public static Vector3 lastPosition;
    public static bool isSpawningOnLastPosition = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _skin = gameObject.GetComponent<Renderer>();
        _skin.material = EquippedSkins.skinMaterials["ball"];
        _skin.material.shader = Shader.Find("Standard");
    }

    private void Start()
    {
        if (isSpawningOnLastPosition)
            transform.position = lastPosition;
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
        
        Vector3 lastPositionTemp = lastPosition;
        Scene currentScene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(currentScene.name);
        
        lastPosition = lastPositionTemp;
        isSpawningOnLastPosition = true;
        
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
