using UnityEngine;

namespace LMO {

    public class PlayerHighJumpEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerJump playerJump;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private Animator animator;
        private HighJumpTrail jumpTrail;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            animator = player.Anim;
            jumpTrail = player.Trail;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerJump == null) {
                return;
            }
            playerJump.OnHighJump += playerVFX.PlayJumpParticles;
            playerJump.OnHighJump += squishy.HighJump.Play;
            playerJump.OnHighJump += TriggerAnimation;
            playerJump.OnHighJump += jumpTrail.StartTrail;
        }

        public void UnsubscribeEvents() {
            if (playerJump == null) {
                return;
            }
            playerJump.OnHighJump -= playerVFX.PlayJumpParticles;
            playerJump.OnHighJump -= squishy.HighJump.Play;
            playerJump.OnHighJump -= TriggerAnimation;
            playerJump.OnHighJump -= jumpTrail.StartTrail;
        }

        private void TriggerAnimation() {
            animator.SetTrigger("Spin");
        }
    }
}