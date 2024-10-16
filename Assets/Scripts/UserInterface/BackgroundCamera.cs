using UnityEngine;

public class BackgroundCamera : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;

    private Quaternion _minPosition;
    private Quaternion _maxPosition;

    private void Start()
    {
        _minPosition = Quaternion.Euler(new Vector3(-15, -10, 0));
        _maxPosition = Quaternion.Euler(new Vector3(15, 10, 0));
    }

    private void Update()
    {
        Quaternion targetRotation = _mainCamera.rotation;

        targetRotation.x = Mathf.Clamp((targetRotation.x - .3f) / 2, _minPosition.x, _maxPosition.x);
        targetRotation.y = Mathf.Clamp(targetRotation.y / 2, _minPosition.y, _maxPosition.y);
        targetRotation.z = 0;

        transform.rotation = targetRotation;
    }
}
