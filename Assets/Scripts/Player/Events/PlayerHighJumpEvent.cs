using UnityEngine;

namespace LMO {

    public class PlayerHighJumpEvent : MonoBehaviour, ICustomEvent {
        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private HighJumpTrail jumpTrail;
        private PlayerAudioManager audioManager;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            jumpTrail = player.Trail;
            audioManager = player.Controller.playerAudioManager;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            PlayerJump.OnHighJump += HighJump;
        }

        public void UnsubscribeEvents() {
            PlayerJump.OnHighJump -= HighJump;

        }

        private void HighJump() {
            playerVFX.PlayJumpParticles();
            squishy.HighJump.Play();
            jumpTrail.StartTrail();
            audioManager.PlayJump();
        }
    }
}