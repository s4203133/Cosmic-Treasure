using UnityEngine;
using UnityEngine.UI;

namespace LMO {

    public class UIManager : MonoBehaviour {
        public static UIManager instance;

        [SerializeField] private Animator fadeScreen;

        void Awake() {
            if (instance != null) {
                Debug.LogWarning("There are multiple UIManagers in the scene, please ensure they is only one", this);
                Destroy(this);
            } else {
                instance = this;
            }
        }

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeFromEvents();
        }

        private void FadeScreenIn() {
            fadeScreen.SetTrigger("FadeIn");
        }

        private void FadeScreenOut() {
            fadeScreen.SetTrigger("FadeOut");
        }

        // When the scene is ending, fade out the screen
        private void SubscribeToEvents() {
            GlobalEventManager.SceneRestarted += FadeScreenIn;
        }

        private void UnsubscribeFromEvents() {
            GlobalEventManager.SceneRestarted -= FadeScreenIn;
        }
    }
}