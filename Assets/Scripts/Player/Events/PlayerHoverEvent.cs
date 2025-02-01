using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerHoverEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerHover playerHover;

        // Observers
        private PlayerVFX playerVFX;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
        }

        // When the player dives, notify other systems so they can respond

        public void SubscribeEvents() {
            if (playerHover == null) {
                return;
            }
            playerHover.OnHoverStarted += playerVFX.PlayHoverVFX;
            playerHover.OnHoverEnded += playerVFX.StopHoverVFX;
        }

        public void UnsubscribeEvents() {
            if (playerHover == null) {
                return;
            }
            playerHover.OnHoverStarted -= playerVFX.PlayHoverVFX;
            playerHover.OnHoverEnded -= playerVFX.StopHoverVFX;
        }
    }
}