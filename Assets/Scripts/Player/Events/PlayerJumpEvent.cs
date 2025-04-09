using UnityEngine;

namespace LMO {

    public class PlayerJumpEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private CameraShaker camShake;
        private PlayerStateMachine stateMachine;
        private HighJumpTrail trail;
        private PlayerMovement movement;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            PlayerController controller = player.Controller;
            playerVFX = controller.playerVFX;
            squishy = controller.playerSquashAndStretch;
            camShake = player.CameraShake;
            stateMachine = controller.playerStateMachine;
            trail = controller.PlayerEffectTrail;
            movement = controller.playerMovment;
        }

        public void SubscribeEvents() {
            PlayerJump.OnJump += PlayerJumpEvents;
            SpringPad.OnSmallSpringJump += PlayerSpringJumpEvents;
        }

        public void UnsubscribeEvents() {
            PlayerJump.OnJump -= PlayerJumpEvents;
            SpringPad.OnSmallSpringJump -= PlayerSpringJumpEvents;
        }

        private void PlayerJumpEvents() {
            squishy.Jump.Play();
            playerVFX.PlayJumpParticles();
            playerVFX.StopRunParticles();
            movement.FinishedMoving();
        }

        private void PlayerSpringJumpEvents() {
            PlayerJumpEvents();
            camShake.shakeTypes.small.Shake();
            stateMachine.Fall();
            trail.StartTrail();
            movement.FinishedMoving();
        }
    }
}