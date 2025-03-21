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
            PlayerHover.OnHoverStarted += playerVFX.PlayHoverVFX;
            PlayerHover.OnHoverContinued += playerVFX.PlayHoverVFX;
            PlayerHover.OnHoverEnded += playerVFX.StopHoverVFX;
            Grounded.OnLanded += hover.EndHover;
            Grounded.OnLanded += playerVFX.StopHoverVFX;
        }

        public void UnsubscribeEvents() {
            PlayerHover.OnHoverStarted -= playerVFX.PlayHoverVFX;
            PlayerHover.OnHoverContinued -= playerVFX.PlayHoverVFX;
            PlayerHover.OnHoverEnded -= playerVFX.StopHoverVFX;
            Grounded.OnLanded -= hover.EndHover;
            Grounded.OnLanded -= playerVFX.StopHoverVFX;
        }
    }
}