using UnityEngine;

namespace LMO {

    public class PlayerSwingEvent : MonoBehaviour, ICustomEvent {

        [Header("Subject")]
        [SerializeField] private PlayerSwing swing;
        private FOVChanger fovChanger;
        private CameraShaker camShake;
        private Animator animator;
        private HighJumpTrail trail;
        private Grapple grapple;
        private SwingRope rope;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            animator = player.Anim;
            fovChanger = player.FOV_Changer;
            camShake = player.CameraShake;
            trail = player.Trail;
            grapple = player.Controller.playerGrapple;
            rope = grapple.Rope;
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
            grapple.OnGrappleStarted -= ConnectGrappleRope;
            grapple.OnGrappleEnded -= EndSwing;
            PlayerSwingState.OnSwingStart -= StartSwing;
            PlayerSwingState.OnSwingEnd -= EndSwing;
            PlayerSwingState.OnJumpFromSwing -= JumpFromSwing;
            Grounded.OnLanded -= Land;
        }

        private void StartSwing() {
            swing.StartSwing(grapple.NearestObject.transform);
            ConnectGrappleRope();
            fovChanger.EndChange();
        }

        private void ConnectGrappleRope() {
            animator.SetBool("Swinging", true);
            rope.SetRopeTarget(grapple.NearestObject.transform);
        }

        private void EndSwing() {
            animator.SetBool("Swinging", false);
            swing.EndSwing();
            rope.DetatchRope();
        }

        private void JumpFromSwing() {
            animator.SetBool("Swinging", true);
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