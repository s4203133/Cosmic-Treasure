using UnityEngine;

public class CursorLock : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor() {
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
