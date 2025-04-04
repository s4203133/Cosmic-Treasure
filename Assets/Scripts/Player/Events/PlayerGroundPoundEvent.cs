using UnityEngine;

namespace LMO {

    public class PlayerGroundPoundEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerGroundPound playerGroundPound;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private CameraShaker cameraShaker;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            cameraShaker = player.CameraShake;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerGroundPound == null) {
                return;
            }
            PlayerGroundPound.OnGroundPoundLanded += playerVFX.PlayGroundPoundParticles;
            PlayerGroundPound.OnGroundPoundLanded += squishy.GroundPound.Play;
            PlayerGroundPound.OnGroundPoundLanded += cameraShaker.shakeTypes.small.Shake;
        }

        public void UnsubscribeEvents() {
            if (playerGroundPound == null) {
                return;
            }
            PlayerGroundPound.OnGroundPoundLanded -= playerVFX.PlayGroundPoundParticles;
            PlayerGroundPound.OnGroundPoundLanded -= squishy.GroundPound.Play;
            PlayerGroundPound.OnGroundPoundLanded -= cameraShaker.shakeTypes.small.Shake;
        }
    }
}