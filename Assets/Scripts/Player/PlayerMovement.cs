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

    public static Vector3 lastPosition;
    public static Vector3 spawnPosition;
    
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
  
        RenderSettings.skybox = EquippedSkins.skinMaterials["background"];
    }

    private void Start()
    {
        if (spawnPosition == null)
            spawnPosition = CheckPoint.checkPoint;

        transform.position = spawnPosition;
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
        // Creating particles under the player at a specified time interval if the player is moving
        if (_rigidbody.velocity.magnitude < .5f)
            return;

        _lastSpawnTrailParticle -= Time.deltaTime;

        if (_lastSpawnTrailParticle > 0)
            return;

        Vector3 position = transform.position;
        position.y -= .5f;

        GameObject trailParticle = Instantiate(_trailParticlePrefab);
        trailParticle.transform.position = position;

        // Applying the material of the block the player is standing on to the particles
        if (collision.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
            trailParticle.GetComponent<ParticleSystemRenderer>().material = renderer.material;

        else 
            trailParticle.GetComponent<ParticleSystemRenderer>().material = collision.transform.Find("Cube").gameObject.GetComponent<Renderer>().material;
        
        _lastSpawnTrailParticle = .2f;
    }

    private void OnCollisionExit(Collision collision)
    {
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
        
        Vector3 lastPositionTemp = lastPosition;
        Scene currentScene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(currentScene.name);

        lastPosition = lastPositionTemp;
        spawnPosition = position;
        
        _restartMenu.SetActive(false);
        _joystickUI.SetActive(true);

        isDead = false;
    }

    private void Move()
    {
        if (Input.touchCount > 0)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            Vector3 movementJoystick = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            Vector3 movement = movementJoystick.x * cameraRight + movementJoystick.z * cameraForward;

            Vector3 rigidbodyVelocity = _rigidbody.velocity;

            rigidbodyVelocity.x = movement.x * Speed;
            rigidbodyVelocity.z = movement.z * Speed;

            Vector3 rigidbodyLerpVelocity = Vector3.Lerp(_rigidbody.velocity, rigidbodyVelocity, Time.fixedDeltaTime * 2f);

            _rigidbody.velocity = rigidbodyLerpVelocity;
        }
    }
}
