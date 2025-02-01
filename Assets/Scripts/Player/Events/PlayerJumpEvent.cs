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

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerJump == null) {
                return;
            }
            playerJump.OnJump += playerVFX.PlayJumpParticles;
            playerJump.OnJump += squishy.Jump.Play;
        }

        public void UnsubscribeEvents() {
            if (playerJump == null) {
                return;
            }
            playerJump.OnJump -= playerVFX.PlayJumpParticles;
            playerJump.OnJump -= squishy.Jump.Play;
        }
    }
}