using UnityEngine;

namespace LMO {

    public class PlayerSpinEvent : MonoBehaviour, ICustomEvent {

        [Header("SUBJECT")]
        [SerializeField] private PlayerSpinAttack playerSpin;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private Animator animator;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            animator = player.Anim;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            if (playerSpin == null) {
                return;
            }
            playerSpin.OnSpin += StartSpin;
            playerSpin.OnSpinEnd += EndSpin;
        }

        public void UnsubscribeEvents() {
            if (playerSpin == null) {
                return;
            }
            playerSpin.OnSpin -= StartSpin;
            playerSpin.OnSpinEnd -= EndSpin;
        }

        private void StartSpin() {
            playerVFX.PlaySpinVFX();
            squishy.SpinAttack.Play();
            animator.SetBool("SpinAttacking", true);
        }

        private void EndSpin() {
            animator.SetBool("SpinAttacking", false);
        }
    }
}