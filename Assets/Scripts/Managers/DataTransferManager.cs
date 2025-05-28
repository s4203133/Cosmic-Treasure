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
        SceneManager.sceneLoaded += LevelLoaded;
        InputHandler.pauseStarted += ToggleReset;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= LevelLoaded;
        InputHandler.pauseStarted -= ToggleReset;
    }

    private void ToggleReset() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            resetVariables = !resetVariables;
        }
    }

    private void LevelLoaded(Scene scene, LoadSceneMode loadMode) {
        if (scene.buildIndex == 0) {
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
