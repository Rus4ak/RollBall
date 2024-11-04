using UnityEngine;

public class Sphere : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -10f)
            Destroy(gameObject);
    }
}
