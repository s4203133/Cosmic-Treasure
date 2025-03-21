using UnityEngine;

namespace LMO {

    public class PlayerSwingEvent : MonoBehaviour, ICustomEvent {

        [Header("Subject")]
        [SerializeField] private PlayerSwing swing;
        [SerializeField] private SwingRope rope;
        private FOVChanger fovChanger;
        private CameraShaker camShake;
        private Animator animator;
        private HighJumpTrail trail;
        private Grapple grapple;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            animator = player.Anim;
            fovChanger = player.FOV_Changer;
            camShake = player.CameraShake;
            trail = player.Trail;
            grapple = player.Controller.playerGrapple;
        }

        public void SubscribeEvents() {
            grapple.OnGrappleStarted += ConnectGrappleRope;
            grapple.OnGrappleEnded += EndSwing;
            PlayerSwingState.OnSwingStart += StartSwing;
            PlayerSwingState.OnSwingEnd += EndSwing;
            PlayerSwingState.OnJumpFromSwing += JumpFromSwing;
            Grounded.OnLanded += Land;
        }

        public void UnsubscribeEvents() {
            grapple.OnGrappleStarted += StartSwing;
            grapple.OnGrappleEnded += EndSwing;
            PlayerSwingState.OnSwingStart -= StartSwing;
            PlayerSwingState.OnSwingEnd -= EndSwing;
            PlayerSwingState.OnJumpFromSwing -= JumpFromSwing;
            Grounded.OnLanded -= Land;
        }

        private void StartSwing(Transform target) {
            swing.StartSwing(target);
            ConnectGrappleRope(target);
            fovChanger.EndChange();
        }

        private void ConnectGrappleRope(Transform target) {
            rope.SetRopeTarget(target);
        }

        private void EndSwing() {
            swing.EndSwing();
            rope.DetatchRope();
        }

        private void JumpFromSwing() {
            animator.SetTrigger("BackFlip");
            trail.StartTrail();
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