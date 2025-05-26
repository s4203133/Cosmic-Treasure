using UnityEngine;

namespace LMO {

    public class PlayerSwingEvent : MonoBehaviour, ICustomEvent {

        [Header("Subject")]
        [SerializeField] private PlayerSwing swing;
        private FOVChanger fovChanger;
        private CameraShaker camShake;
        private HighJumpTrail trail;
        private Grapple grapple;
        private SwingRope rope;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            fovChanger = player.FOV_Changer;
            camShake = player.CameraShake;
            trail = player.Trail;
            grapple = player.Controller.playerGrapple;
            rope = grapple.Rope;
        }

        public void SubscribeEvents() {
            Grapple.OnGrappleStarted += ConnectGrappleRope;
            Grapple.OnGrapplePulled += PullGrappleRope;
            Grapple.OnGrappleEnded += EndSwing;
            PlayerSwingState.OnSwingStart += StartSwing;
            PlayerSwingState.OnSwingEnd += EndSwing;
            PlayerSwingState.OnJumpFromSwing += JumpFromSwing;
            Grounded.OnLanded += Land;
            PlayerDeath.OnPlayerDied += EndSwing;
        }

        public void UnsubscribeEvents() {
            Grapple.OnGrappleStarted -= ConnectGrappleRope;
            Grapple.OnGrapplePulled -= PullGrappleRope;
            Grapple.OnGrappleEnded -= EndSwing;
            PlayerSwingState.OnSwingStart -= StartSwing;
            PlayerSwingState.OnSwingEnd -= EndSwing;
            PlayerSwingState.OnJumpFromSwing -= JumpFromSwing;
            Grounded.OnLanded -= Land;
            PlayerDeath.OnPlayerDied -= EndSwing;
        }

        private void StartSwing() {
            swing.StartSwing(grapple.NearestObject.transform);
            ConnectGrappleRope();
            fovChanger.EndChange();
        }

        private void ConnectGrappleRope() {
            rope.SetRopeTarget(grapple.NearestObject.transform);
        }

        private void PullGrappleRope() {
            camShake.shakeTypes.medium.Shake();
        }

        private void EndSwing() {
            swing.EndSwing();
            rope.DetatchRope();
        }

        private void JumpFromSwing() {
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