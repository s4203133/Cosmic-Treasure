using LMO.Interfaces;
using LMO.Player;
using UnityEngine;

namespace LMO.CustomEvents {

    public class PlayerSwingEvent : MonoBehaviour, ICustomEvent {

        [Header("Subject")]
        [SerializeField] private PlayerSwing swing;
        [SerializeField] private SwingRope rope;
        private FOVChanger fovChanger;
        private CameraShaker camShake;
        private Grounded grounded;


        private Animator animator;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            animator = player.Anim;
            fovChanger = player.FOV_Changer;
            camShake = player.CameraShake;
            grounded = player.Controller.playerJump.groundedSystem;
        }

        public void SubscribeEvents() {
            PlayerSwingState.OnSwingStart += StartSwing;
            PlayerSwingState.OnSwingEnd += EndSwing;
            PlayerSwingState.OnJumpFromSwing += JumpFromSwing;
            grounded.OnLanded += Land;
        }

        public void UnsubscribeEvents() {
            PlayerSwingState.OnSwingStart -= StartSwing;
            PlayerSwingState.OnSwingEnd -= EndSwing;
            PlayerSwingState.OnJumpFromSwing -= JumpFromSwing;
            grounded.OnLanded -= Land;
        }

        private void StartSwing(Vector3 targetPos) {
            swing.StartSwing(targetPos);
            rope.SetRopeTarget(targetPos);
            fovChanger.EndChange();
        }

        private void EndSwing() {
            swing.EndSwing();
            rope.DetatchRope();
        }

        private void JumpFromSwing() {
            animator.SetTrigger("BackFlip");
            fovChanger.StartChange();
        }

        private void Land() {
            if (!swing.Landed) {
                camShake.shakeTypes.medium.Shake();
                swing.RegisterLanded();
            }
        }
    }
}