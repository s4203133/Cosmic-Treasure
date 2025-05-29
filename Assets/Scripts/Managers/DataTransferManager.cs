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

        if(scene.buildIndex != 2) {
            sceneIndexData.currentLevel = scene.buildIndex;
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

[System.Serializable]
public class k {

    private int totalCoins;
    private int totalGems;
    private int totalRabbits;
    private int totalEnemies;

    public int coins;
    public int gems;
    public int rabbits;
    public int enemiesEliminated;
}
