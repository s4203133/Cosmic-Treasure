using UnityEngine;

namespace LMO {

    public class PlayerDiveEvent : MonoBehaviour, ICustomEvent {
        [Header("SUBJECT")]
        [SerializeField] private PlayerDive playerDive;

        // Observers
        private PlayerVFX playerVFX;
        private PlayerSquashAndStretch squishy;
        private Animator animator;
        private HighJumpTrail jumpTrail;
        private FOVChanger fovChanger;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            playerVFX = player.VFX;
            squishy = player.SqashAndStretch;
            animator = player.Anim;
            jumpTrail = player.Trail;
            fovChanger = player.FOV_Changer;
        }

        // When the player dives, notify other systems so they can respond
        public void SubscribeEvents() {
            playerDive.OnDive += OnPLayerDive;
        }

        public void UnsubscribeEvents() {
            playerDive.OnDive -= OnPLayerDive;
        }

        private void OnPLayerDive() {
            animator.SetTrigger("Dive");
            playerVFX.PlayDiveVFX();
            squishy.Dive.Play();
            jumpTrail.StartTrail();
            fovChanger.StartChange();
        }
    }
}