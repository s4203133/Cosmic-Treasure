using UnityEngine;
using LMO;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GamepadUIButton startButton;
    private PlayerStateMachine player;

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
        pauseMenu.SetActive(true);
        startButton.Highlight();
        InputHandler.Disable();
        player.Deactivate();
        Time.timeScale = 0f;
    }

    public void Resume() {
        InputHandler.Enable();
        player.Activate();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit() {
        Time.timeScale = 1f;
        SceneLoadManager.instance.LoadScene(0);
    }
}
