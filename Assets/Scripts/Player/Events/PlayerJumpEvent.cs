using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

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
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            camShake = player.CameraShake;
            stateMachine = player.Controller.playerStateMachine;
            animator = player.Anim;
            trail = player.Trail;
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
        }

        private void PlayerSpringJumpEvents() {
            PlayerJumpEvents();
            camShake.shakeTypes.small.Shake();
            stateMachine.Fall();
            animator.SetTrigger("Spin");
            trail.StartTrail();
        }
    }
}