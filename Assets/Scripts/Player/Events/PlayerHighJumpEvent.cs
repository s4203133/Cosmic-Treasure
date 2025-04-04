using UnityEngine;

namespace LMO {

    public class PlayerHighJumpEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private HighJumpTrail jumpTrail;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            jumpTrail = player.Trail;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            PlayerJump.OnHighJump += playerVFX.PlayJumpParticles;
            PlayerJump.OnHighJump += squishy.HighJump.Play;
            PlayerJump.OnHighJump += jumpTrail.StartTrail;
        }

        public void UnsubscribeEvents() {
            PlayerJump.OnHighJump -= playerVFX.PlayJumpParticles;
            PlayerJump.OnHighJump -= squishy.HighJump.Play;
            PlayerJump.OnHighJump -= jumpTrail.StartTrail;
        }
    }
}