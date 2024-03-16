using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private GameObject _restartMenu;
    [SerializeField] private Transform _camera;

    private Vector3 _checkPoint;
    private Vector3 _lastPosition;
    
    public static Vector3 resetPosition = CheckPoint.checkPoint;

    private void Start()
    {
        _checkPoint = CheckPoint.checkPoint;
    }

    private void Update()
    {
        if (transform.position.y < -10f)
        {
            _restartMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.isStatic)
        {
            _lastPosition = collision.gameObject.transform.position;
            _lastPosition.y += 1f;
        }
    }

    public void RestartToSpawnpoint()
    {
        _checkPoint = CheckPoint.checkPoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        resetPosition = _checkPoint;
        CheckPoint.checkPoint = _checkPoint;

    }

    public void RestartToLastPosition()
    {
        _checkPoint = CheckPoint.checkPoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        resetPosition = _lastPosition;
        CheckPoint.checkPoint = _checkPoint;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = resetPosition;

        Vector3 cameraPosition = resetPosition;
        cameraPosition.y += 2f;
        cameraPosition.z -= 5f;

        _camera.position = cameraPosition;

        Time.timeScale = 1f;
    }
}
