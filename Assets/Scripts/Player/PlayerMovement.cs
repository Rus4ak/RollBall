using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;
    [SerializeField] private GameObject _restartMenu;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private GameObject _trailParticlePrefab;

    private Rigidbody _rigidbody;
    private GameObject _joystickUI;
    private float _lastSpawnTrailParticle;

    private bool _isSlice = false;
    private bool _isStopBall = true;

    [NonSerialized] public bool isCollision;

    public static Vector3 lastPosition;
    public static Vector3 spawnPosition;

    public static bool _isStop = false;
    public static bool isDead = false;

    public float Speed
    {
        get => _speed;

        set => _speed = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _joystickUI = GameObject.FindGameObjectWithTag("Joystick");
    }

    private void Start()
    {
        if (spawnPosition == null)
            spawnPosition = CheckPoint.checkPoint;

        transform.position = spawnPosition;

        if (_restartMenu.activeInHierarchy)
            _restartMenu.SetActive(false);
    }

    private void FixedUpdate()
    {
        Move();
        
        if (isDead)
        {
            _restartMenu.SetActive(true);
            _joystickUI.SetActive(false);

            Time.timeScale = 0;
        }

        if (transform.position.y < -10f)
        {
            isDead = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ice"))
        {
            _isSlice = true;
            _isStopBall = false;
        }

        if (isCollision == false)
            isCollision = true;

        if (Quality.quality == 1)
            return;

        // Creating particles under the player at a specified time interval if the player is moving
        if (_rigidbody.velocity.magnitude < .5f)
            return;

        _lastSpawnTrailParticle -= Time.deltaTime;

        if (_lastSpawnTrailParticle > 0)
            return;

        Vector3 position = transform.position;
        position.y -= .5f;

        // Create the particles under the player
        GameObject trailParticle = Instantiate(_trailParticlePrefab);
        trailParticle.transform.position = position;

        // Applying the material of the block the player is standing on to the particles
        if (collision.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
            trailParticle.GetComponent<ParticleSystemRenderer>().material = renderer.material;

        else if (collision.transform.Find("Cube"))
            if (collision.transform.Find("Cube").gameObject.TryGetComponent<Renderer>(out Renderer cubeRenderer))
                trailParticle.GetComponent<ParticleSystemRenderer>().material = cubeRenderer.material;
        
        _lastSpawnTrailParticle = .5f / _rigidbody.velocity.magnitude;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ice"))
        {
            _isSlice = false;
            _isStopBall = true;
        }

        isCollision = false;

        if (collision.gameObject.layer == 10)
            return;

        // Saving the position of the last block the player was on
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

    // Restarting the game and moving the player to a given position
    public void Respawn(Vector3 position)
    {
        Time.timeScale = 1;

        float passingTime = Finish.passingTime;
        Vector3 lastPositionTemp = lastPosition;
        Scene currentScene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(currentScene.name);
        
        lastPosition = lastPositionTemp;
        spawnPosition = position;
        Finish.passingTime = passingTime;

        _restartMenu.SetActive(false);
        _joystickUI.SetActive(true);

        isDead = false;
    }

    private void Move()
    {
        if (!_isStop)
        {
            if (Input.touchCount > 0)
            {
                Vector3 cameraForward = Camera.main.transform.forward;
                Vector3 cameraRight = Camera.main.transform.right;

                Vector3 movementJoystick = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
                Vector3 movement = movementJoystick.x * cameraRight + movementJoystick.z * cameraForward;
                
                Vector3 rigidbodyVelocity = _rigidbody.velocity;

                if (_isSlice)
                {
                    Vector3 targetVelocity = movement * Speed;

                    rigidbodyVelocity.x = Mathf.Lerp(rigidbodyVelocity.x, targetVelocity.x, Time.fixedDeltaTime * 10f);
                    rigidbodyVelocity.z = Mathf.Lerp(rigidbodyVelocity.z, targetVelocity.z, Time.fixedDeltaTime * 10f);
                }
                else
                {
                    rigidbodyVelocity.x = movement.x * Speed;
                    rigidbodyVelocity.z = movement.z * Speed;
                }

                Vector3 rigidbodyLerpVelocity = Vector3.Lerp(_rigidbody.velocity, rigidbodyVelocity, Time.fixedDeltaTime * 2f);

                _rigidbody.velocity = rigidbodyLerpVelocity;
            }
            else
            {
                // Stop the ball when releasing the screen
                if (isCollision)
                    if (_isStopBall)
                        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, Time.fixedDeltaTime * 2f);
            }
        }
        else
        {
            if (!_rigidbody.IsSleeping())
                _rigidbody.Sleep();
        }
    }
}
