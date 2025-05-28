using UnityEngine;

namespace LMO {

    public class PlayerGroundPoundEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerGroundPound playerGroundPound;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private CameraShaker cameraShaker;
        private PlayerAudioManager audioManager;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            cameraShaker = player.CameraShake;
            audioManager = player.Controller.playerAudioManager;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerGroundPound == null) {
                return;
            }
            PlayerGroundPound.OnGroundPoundInitialised += audioManager.PlayGroundPound;
            PlayerGroundPound.OnGroundPoundLanded += GroundPoundLand;
        }

        public void UnsubscribeEvents() {
            if (playerGroundPound == null) {
                return;
            }
            PlayerGroundPound.OnGroundPoundInitialised -= audioManager.PlayGroundPound;
            PlayerGroundPound.OnGroundPoundLanded -= GroundPoundLand;
        }

        private void GroundPoundLand() {
            playerVFX.PlayGroundPoundParticles();
            squishy.GroundPound.Play();
            cameraShaker.shakeTypes.small.Shake();
            audioManager.PlayGroundPoundLand();
        }
    }
}