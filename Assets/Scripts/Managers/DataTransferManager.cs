using LMO;
using NR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataTransferManager : MonoBehaviour
{
    [SerializeField] private SceneIndexData sceneIndexData;
    LevelSaveManager levelSave;
    private bool resetVariables;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        resetVariables = false;
    }

    private void OnEnable() {
        InputHandler.pauseStarted += ToggleReset;
        //Cannon.OnCannonLaunched += GetCurrentLevel;
    }

    private void OnDisable() {
        InputHandler.pauseStarted -= ToggleReset;
        //Cannon.OnCannonLaunched -= GetCurrentLevel;
    }

    private void ToggleReset() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            resetVariables = !resetVariables;
        }
    }

    private void OnLevelWasLoaded(int level) {
        if (level == 0) {
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
    
    private void GetCurrentLevel() {
        sceneIndexData.currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
}
