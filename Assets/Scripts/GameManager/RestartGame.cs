using System;
using UnityEngine;

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
