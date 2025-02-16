using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerHoverEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
        }

        public void SubscribeEvents() {
            PlayerHover.OnHoverStarted += playerVFX.PlayHoverVFX;
            PlayerHover.OnHoverEnded += playerVFX.StopHoverVFX;
        }

        public void UnsubscribeEvents() {
            PlayerHover.OnHoverStarted -= playerVFX.PlayHoverVFX;
            PlayerHover.OnHoverEnded -= playerVFX.StopHoverVFX;
        }
    }
}