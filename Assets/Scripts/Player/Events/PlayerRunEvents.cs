using UnityEngine;

namespace LMO {

    public class PlayerRunEvents : MonoBehaviour, ICustomEvent {

        // Observers
        private PlayerVFX playerVFX;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            PlayerController controller = player.Controller;

            playerVFX = controller.playerVFX;
        }

        public void SubscribeEvents() {
            PlayerMovement.OnMoveStarted += StartRun;
            PlayerMovement.OnMoveStopped += StopRun;
        }

        public void UnsubscribeEvents() {
            PlayerMovement.OnMoveStarted -= StartRun;
            PlayerMovement.OnMoveStopped -= StopRun;
        }

        private void StartRun() {
            playerVFX.StartRunParticles();
        }

        private void StopRun() {
            playerVFX?.StopRunParticles();
        }
    }
}