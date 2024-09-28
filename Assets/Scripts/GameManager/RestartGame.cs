using System;
using UnityEngine;

// Calling the game restart event depending on the selected restart method
public class RestartGame : MonoBehaviour
{
    public static event Action<Vector3> PlayerDied;

    public void RestartToCheckPoint()
    {
        PlayerDied?.Invoke(CheckPoint.checkPoint);
    }

    public void RestartToLastPosition()
    {
        PlayerDied?.Invoke(PlayerMovement.lastPosition);
    }
}
