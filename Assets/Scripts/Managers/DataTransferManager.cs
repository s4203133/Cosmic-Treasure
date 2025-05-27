using LMO;
using NR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataTransferManager : MonoBehaviour
{
    LevelSaveManager levelSave;
    private bool resetVariables;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        resetVariables = false;
    }

    private void OnEnable() {
        InputHandler.pauseStarted += ToggleReset;
    }

    private void OnDisable() {
        InputHandler.pauseStarted -= ToggleReset;
    }

    private void ToggleReset() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            resetVariables = !resetVariables;
        }
    }

    private void OnLevelWasLoaded(int level) {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            resetVariables = false;
            return;
        }

        if (resetVariables) {
            ResetCollectibles();
        }
    }

    private void ResetCollectibles() {
        levelSave = FindObjectOfType<LevelSaveManager>();
        if (levelSave != null) {
            levelSave.ClearData();
        }
    }
}
