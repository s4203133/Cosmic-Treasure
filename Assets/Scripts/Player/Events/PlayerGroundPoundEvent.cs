using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerGroundPoundEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerGroundPound playerGroundPound;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private Animator animator;
        private CameraShaker cameraShaker;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            animator = player.Anim;
            cameraShaker = player.CameraShake;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerGroundPound == null) {
                return;
            }
            playerGroundPound.OnGroundPoundInitialised += AnimateSpin;
            playerGroundPound.OnGroundPoundLanded += playerVFX.PlayGroundPoundParticles;
            playerGroundPound.OnGroundPoundLanded += squishy.GroundPound.Play;
            playerGroundPound.OnGroundPoundLanded += cameraShaker.shakeTypes.small.Shake;
        }

        public void UnsubscribeEvents() {
            if (playerGroundPound == null) {
                return;
            }
            playerGroundPound.OnGroundPoundInitialised -= AnimateSpin;
            playerGroundPound.OnGroundPoundLanded -= playerVFX.PlayGroundPoundParticles;
            playerGroundPound.OnGroundPoundLanded -= squishy.GroundPound.Play;
            playerGroundPound.OnGroundPoundLanded -= cameraShaker.shakeTypes.small.Shake;
        }

        private void AnimateSpin() {
            animator.SetTrigger("StartGroundPound");
        }
    }
}