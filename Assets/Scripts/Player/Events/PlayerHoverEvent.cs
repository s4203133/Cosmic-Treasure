using UnityEngine;

namespace LMO {

    public class PlayerHoverEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerHover hover;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            hover = player.Controller.playerHover;
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
        }

        private void HoverEnded() {
            playerVFX.StopHoverVFX();
        }

        private void LandedOnGround() {
            hover.EndHover();
            playerVFX.StopHoverVFX();
        }
    }
}