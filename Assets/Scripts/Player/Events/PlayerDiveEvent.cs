using UnityEngine;
using LMO.Interfaces;
using LMO.Player;

namespace LMO.CustomEvents {

    public class PlayerDiveEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerDive playerDive;

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
            if (playerDive == null) {
                return;
            }
            playerDive.OnDive += TriggerAnimation;
            playerDive.OnDive += playerVFX.PlayDiveVFX;
            playerDive.OnDive += squishy.Dive.Play;
            playerDive.OnDive += jumpTrail.StartTrail;
        }

        public void UnsubscribeEvents() {
            if (playerDive == null) {
                return;
            }
            playerDive.OnDive -= TriggerAnimation;
            playerDive.OnDive -= playerVFX.PlayDiveVFX;
            playerDive.OnDive -= squishy.Dive.Play;
            playerDive.OnDive -= jumpTrail.StartTrail;
        }

        private void TriggerAnimation() {
            animator.SetTrigger("Dive");
        }
    }
}