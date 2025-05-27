using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LMO {

    public class SceneLoadManager : MonoBehaviour {

        public static SceneLoadManager instance;
        [SerializeField] private float loadDelay;
        private WaitForSeconds delay;

        [SerializeField] private SceneIndexData sceneIndexData;

        public static Action OnSceneStartedLoading;

        void Awake() {
            if (instance != null) {
                Debug.LogWarning("There are multiple SceneLoadManagers in the scene, please ensure they is only one", this);
                Destroy(this);
            } else {
                instance = this;
            }

            delay = new WaitForSeconds(loadDelay);
        }

        private void OnEnable() {
            GlobalEventManager.SceneRestarted += RestartScene;
        }

        private void OnDisable() {
            GlobalEventManager.SceneRestarted -= RestartScene;
        }

        public void RestartScene() {
            // Event to fade out screen and trigger any clean up logic before scene ends
            OnSceneStartedLoading?.Invoke();

            // Load the scene after a delay
            StartCoroutine(DelayLoad());
            IEnumerator DelayLoad() {
                yield return delay;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public void LoadScene(int sceneIndex) {
            // Event to fade out screen and trigger any clean up logic before scene ends
            OnSceneStartedLoading?.Invoke();

            // Load the scene after a delay
            StartCoroutine(DelayLoad());
            IEnumerator DelayLoad() {
                yield return delay;
                SceneManager.LoadScene(sceneIndex);
            }
        }

        public void LoadNextScene() {
            switch (sceneIndexData.currentLevel) {
                case (1):
                    LoadScene(6);
                    break;
                case (6):
                    LoadScene(8);
                    break;
            }
        }
    }
}