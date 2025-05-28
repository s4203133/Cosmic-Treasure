using UnityEngine;

namespace LMO {

    public class PlayerHoverEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerHover hover;
        private PlayerAudioManager audioManager;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            hover = player.Controller.playerHover;
            audioManager = player.Controller.playerAudioManager;
        }

        public void SubscribeEvents() {
            PlayerHover.OnHoverStarted += HoverStarted;
            PlayerHover.OnHoverContinued += HoverStarted;
            PlayerHover.OnHoverEnded += HoverEnded;
            InputHandler.jumpCancelled += HoverEnded;
            Grounded.OnLanded += LandedOnGround;
        }

        public void UnsubscribeEvents() {
            PlayerHover.OnHoverStarted -= HoverStarted;
            PlayerHover.OnHoverContinued -= HoverStarted;
            PlayerHover.OnHoverEnded -= HoverEnded;
            InputHandler.jumpCancelled -= HoverEnded;
            Grounded.OnLanded -= LandedOnGround;

        }

        private void HoverStarted() {
            playerVFX.PlayHoverVFX();
            audioManager.PlayHover();
        }

        private void HoverEnded() {
            playerVFX.StopHoverVFX();
        }

        private void LandedOnGround() {
            hover.EndHover();
            playerVFX.StopHoverVFX();
            audioManager.StopHover();
        }
    }
}