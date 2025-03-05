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

        public void Initialise(EventManager manager) {
            PlayerEventManager player = manager as PlayerEventManager;
            animator = player.Anim;
            fovChanger = player.FOV_Changer;
            camShake = player.CameraShake;
            trail = player.Trail;
        }

        public void SubscribeEvents() {
            PlayerSwingState.OnSwingStart += StartSwing;
            PlayerSwingState.OnSwingEnd += EndSwing;
            PlayerSwingState.OnJumpFromSwing += JumpFromSwing;
            Grounded.OnLanded += Land;
        }

        public void UnsubscribeEvents() {
            PlayerSwingState.OnSwingStart -= StartSwing;
            PlayerSwingState.OnSwingEnd -= EndSwing;
            PlayerSwingState.OnJumpFromSwing -= JumpFromSwing;
            Grounded.OnLanded -= Land;
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