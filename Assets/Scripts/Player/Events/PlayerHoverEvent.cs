using UnityEngine;

namespace LMO {

    public class PlayerHoverEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerHover hover;
        private Animator animator;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            hover = player.Controller.playerHover;
            animator = player.Controller.playerAnimator;
        }

        public void SubscribeEvents() {
            PlayerHover.OnHoverStarted += HoverStated;
            PlayerHover.OnHoverContinued += HoverStated;
            PlayerHover.OnHoverEnded += HoverEnded;
            InputHandler.jumpCancelled += HoverEnded;
            Grounded.OnLanded += LandedOnGround;
        }

        public void UnsubscribeEvents() {
            PlayerHover.OnHoverStarted -= HoverStated;
            PlayerHover.OnHoverContinued -= HoverStated;
            PlayerHover.OnHoverEnded -= HoverEnded;
            InputHandler.jumpCancelled -= HoverEnded;
            Grounded.OnLanded -= LandedOnGround;

        }

        private void HoverStated() {
            playerVFX.PlayHoverVFX();
            animator.SetBool("Hovering", true);
        }

        private void HoverEnded() {
            playerVFX.StopHoverVFX();
            animator.SetBool("Hovering", false);
        }

        private void LandedOnGround() {
            animator.SetBool("Hovering", false);
            hover.EndHover();
            playerVFX.StopHoverVFX();
        }
    }
}