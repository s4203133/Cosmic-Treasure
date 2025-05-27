using UnityEngine;

namespace LMO {

    public class PauseManager : MonoBehaviour {

        private void OnEnable() {
            SubscribeInput();
        }

        private void OnDisable() {
            UnsubscribeInput();
        }

        private void SubscribeInput() {
            //InputHandler.quitStarted += GlobalEventManager.OnSceneRestarted;
        }

        private void UnsubscribeInput() {
            //InputHandler.quitStarted -= GlobalEventManager.OnSceneRestarted;

        }
    }
}