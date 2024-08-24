using UnityEngine;

public class TutorialDisable : MonoBehaviour
{
    public void Update()
    {
        if (Input.touchCount > 0)
            gameObject.SetActive(false);
    }
}
