using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;
    [SerializeField] private GameObject _restartMenu;
    [SerializeField] private FloatingJoystick _joystick;
  
    private Rigidbody _rigidbody;
    private GameObject _joystickUI;

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
