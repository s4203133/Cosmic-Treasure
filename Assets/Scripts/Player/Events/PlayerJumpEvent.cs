using UnityEngine;

namespace LMO {

    public class PlayerJumpEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerJump playerJump;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private CameraShaker camShake;
        private PlayerStateMachine stateMachine;
        private Animator animator;
        private HighJumpTrail trail;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            PlayerController controller = player.Controller;
            playerVFX = controller.playerVFX;
            squishy = controller.playerSquashAndStretch;
            camShake = player.CameraShake;
            stateMachine = controller.playerStateMachine;
            animator = controller.playerAnimator;
            trail = controller.PlayerEffectTrail;
        }

        public void SubscribeEvents() {
            playerJump.OnJump += PlayerJumpEvents;
            SpringPad.OnPlayerJumpedOffSpring += PlayerSpringJumpEvents;
        }

        public void UnsubscribeEvents() {
            playerJump.OnJump -= PlayerJumpEvents;
            SpringPad.OnPlayerJumpedOffSpring -= PlayerSpringJumpEvents;
        }

        private void PlayerJumpEvents() {
            squishy.Jump.Play();
            playerVFX.PlayJumpParticles();
            animator.SetBool("Jumping", true);
        }

        private void PlayerSpringJumpEvents() {
            PlayerJumpEvents();
            camShake.shakeTypes.small.Shake();
            stateMachine.Fall();
            //animator.SetTrigger("Spin");
            trail.StartTrail();
        }
    }
}