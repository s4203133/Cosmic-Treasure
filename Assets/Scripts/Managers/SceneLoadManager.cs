using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;
    [SerializeField] private float loadDelay;

    public delegate void SceneLoadEvent();
    public SceneLoadEvent SceneStartedLoading;

    void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("There are multiple SceneLoadManagers in the scene, please ensure they is only one", this);
            Destroy(this);
        } else {
            instance = this;
        }
    }

    private void OnEnable() {
        GlobalEventManager.SceneRestarted += RestartScene;
    }

    private void OnDisable() {
        GlobalEventManager.SceneRestarted -= RestartScene;
    }

    public void RestartScene() {
        SceneStartedLoading?.Invoke();
        StartCoroutine(DelayLoad());

        IEnumerator DelayLoad() {
            yield return new WaitForSeconds(loadDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadScene(int sceneIndex) {
        SceneStartedLoading?.Invoke();
        StartCoroutine(DelayLoad());

        IEnumerator DelayLoad() {
            yield return new WaitForSeconds(loadDelay);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
