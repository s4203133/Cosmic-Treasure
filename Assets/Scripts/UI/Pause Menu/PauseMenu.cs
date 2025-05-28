using UnityEngine;
using LMO;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GamepadUIButton startButton;
    private PlayerStateMachine player;

    private bool isQuitting;

    private void Start() {
        player = FindObjectOfType<PlayerStateMachine>();    
    }

    private void OnEnable() {
        InputHandler.quitStarted += ShowPauseMenu;
    }

    private void OnDisable() {
        InputHandler.quitStarted -= ShowPauseMenu;
    }

    private void ShowPauseMenu() {
        if (isQuitting) { return; }
        pauseMenu.SetActive(true);
        startButton.Highlight();
        InputHandler.Disable();
        player.Deactivate();
        Time.timeScale = 0f;
    }

    public void Resume() {
        if (isQuitting) { return; }
        InputHandler.Enable();
        player.Activate();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit() {
        Time.timeScale = 1f;
        isQuitting = true;
        SceneLoadManager.instance.LoadScene(0);
    }
}
