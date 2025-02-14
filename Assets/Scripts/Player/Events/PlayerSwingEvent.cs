using LMO.Interfaces;
using LMO.Player;
using UnityEngine;

namespace LMO.CustomEvents {

    public class PlayerSwingEvent : MonoBehaviour, ICustomEvent {

        [Header("Subject")]
        [SerializeField] private PlayerSwing swing;
        [SerializeField] private SwingRope rope;
        private Grounded grounded;


        private Animator animator;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            animator = player.Anim;
            grounded = player.Controller.playerJump.groundedSystem;
        }

        public void SubscribeEvents() {
            PlayerSwingState.OnSwingStart += swing.StartSwing;
            PlayerSwingState.OnSwingStart += rope.SetRopeTarget;
            PlayerSwingState.OnSwingEnd += swing.EndSwing;
            PlayerSwingState.OnSwingEnd += rope.DetatchRope;
            PlayerSwingState.OnJumpFromSwing += JumpFromSwingAnimation;
            grounded.OnLanded += swing.EndMomentum;
        }

        public void UnsubscribeEvents() {
            PlayerSwingState.OnSwingStart -= swing.StartSwing;
            PlayerSwingState.OnSwingStart -= rope.SetRopeTarget;
            PlayerSwingState.OnSwingEnd -= swing.EndSwing;
            PlayerSwingState.OnSwingEnd -= rope.DetatchRope;
            PlayerSwingState.OnJumpFromSwing -= JumpFromSwingAnimation;
            grounded.OnLanded -= swing.EndMomentum;
        }

        private void JumpFromSwingAnimation() {
            animator.SetTrigger("BackFlip");
        }
    }
}