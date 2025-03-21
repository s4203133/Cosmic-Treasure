using UnityEngine;
using UnityEngine.UI;

namespace LMO {

    public class UIManager : MonoBehaviour {
        public static UIManager instance;

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

        private void SubscribeToEvents() {
        }

        private void UnsubscribeFromEvents() {
        }
    }
}