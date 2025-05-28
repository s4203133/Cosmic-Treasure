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
        private PlayerAudioManager audioManager;

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            fovChanger = player.FOV_Changer;
            camShake = player.CameraShake;
            trail = player.Trail;
            grapple = player.Controller.playerGrapple;
            rope = grapple.Rope;
            audioManager = player.Controller.playerAudioManager;
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
            audioManager.PlaySwing();
        }

        private void ConnectGrappleRope() {
            rope.SetRopeTarget(grapple.NearestObject.transform);
            audioManager.PlayShootRope();
        }

        private void PullGrappleRope() {
            camShake.shakeTypes.medium.Shake();
            audioManager.PlayFireSlingShot();
        }

        private void EndSwing() {
            swing.EndSwing();
            rope.DetatchRope();
            audioManager.PlayReleaseRope();
        }

        private void JumpFromSwing() {
            trail.StartTrail();
            fovChanger.StartChange();
            audioManager.PlayJumpFromRope();
        }

        private void Land() {
            if (!swing.Landed) {
                camShake.shakeTypes.medium.Shake();
                swing.RegisterLanded();
            }
        }
    }
}