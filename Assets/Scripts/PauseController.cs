using Unity.VisualScripting;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; } = false; //Game starting not paused

    public static void SetPause(bool pause)
    {
        IsGamePaused = pause;
    }
}
