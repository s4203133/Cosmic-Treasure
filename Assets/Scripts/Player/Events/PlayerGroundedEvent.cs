using UnityEngine;

namespace LMO {

    public class PlayerGroundedEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private Grounded grounded;
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private PlayerSpinAttack playerSpin;
        private PlayerHover playerHover;
        private FOVChanger fovChanger;
        private Animator animator;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            grounded = player.Controller.playerJump.groundedSystem;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            playerSpin = player.Controller.playerSpinAttack;
            playerHover = player.Controller.playerHover;
            fovChanger = player.FOV_Changer;
            animator = player.Controller.playerAnimator;
        }

        public void SubscribeEvents() {
            Grounded.OnLanded += OnPLayerLanded;
        }

        public void UnsubscribeEvents() {
            Grounded.OnLanded -= OnPLayerLanded;
        }

        public void OnPLayerLanded() {
            grounded.AttatchPlayerToMovingPlatform();
            playerVFX.PlayLandParticles();
            squishy.Land.Play();
            animator.SetBool("Jumping", false);
            playerSpin.ResetAirSpins();
            playerHover.EnableHover();
            fovChanger.EndChange();
        }
    }
}