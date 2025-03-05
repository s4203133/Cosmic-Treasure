using UnityEngine;

namespace LMO {

    public class PlayerRunEvents : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerMovement playerMovement;

        // Observers
        private PlayerVFX playerVFX;
        private Animator animator;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            PlayerController controller = player.Controller;

            playerVFX = controller.playerVFX;
            animator = controller.playerAnimator;
        }

        public void SubscribeEvents() {
            if (playerMovement == null) {
                return;
            }
            playerMovement.OnMoveStarted += StartRun;
            playerMovement.OnMoveStopped += StopRun;
        }

        public void UnsubscribeEvents() {
            if (playerMovement == null) {
                return;
            }
            playerMovement.OnMoveStarted -= StartRun;
            playerMovement.OnMoveStopped -= StopRun;
        }

        private void StartRun() {
            playerVFX.StartRunParticles();
            animator.SetFloat("Speed", 1);
        }

        private void StopRun() {
            playerVFX?.StopRunParticles();
            animator.SetFloat("Speed", 0);
        }
    }
}