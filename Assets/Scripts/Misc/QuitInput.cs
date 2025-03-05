using LMO;
using UnityEngine;

public class QuitInput : MonoBehaviour
{
    private void OnEnable() {
        InputHandler.quitStarted += CloseGame;
    }

    private void OnDisable() {
        InputHandler.quitStarted -= CloseGame;
    }

    private void CloseGame() {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
