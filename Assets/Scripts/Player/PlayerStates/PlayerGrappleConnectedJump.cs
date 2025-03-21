using UnityEngine;

namespace LMO {

    public class PlayerGrappleConnectedJump : PlayerJumpState {

        private SwingManager swingManager;
        private Grounded grounded;
        private Transform thisTransform;
        private Grapple grapple;

        private float timer;

        public PlayerGrappleConnectedJump(PlayerController playerController) : base(playerController) {
            swingManager = context.playerSwingManager;
            grounded = playerController.playerJump.groundedSystem;
            thisTransform = playerController.transform;
            grapple = playerController.playerGrapple;
        }

        public override void OnStateEnter() {
            InputHandler.jumpCancelled += jump.CutOffJump;
            InputHandler.SpinStarted += Spin;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            timer = 0.5f;
            StartJump();
        }

        public override void OnStateExit() {
            InputHandler.jumpCancelled -= jump.CutOffJump;
            InputHandler.SpinStarted -= Spin;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            jump.EndJump();
        }

        public override void OnStateUpdate() {
            LookAtGrappleTarget();
            CheckIfPlatformIsUnderneathPlayer();
            base.OnStateUpdate();
        }

        protected override void Idle() {
            stateMachine.ChangeState(stateMachine.grappleIdle);
        }

        protected override void Run() {
            stateMachine.ChangeState(stateMachine.grappleRun);
        }

        protected override void Spin() {
            grapple.OnGrappleEnded?.Invoke();
            stateMachine.ChangeState(stateMachine.spinState);
        }

        private void LookAtGrappleTarget() {
            Vector3 targetDirection = swingManager.SwingTarget.transform.position;
            targetDirection.y = thisTransform.position.y;
            thisTransform.LookAt(targetDirection);
        }

        private void CheckIfPlatformIsUnderneathPlayer() {
            RaycastHit hit;
            if(Physics.Raycast(thisTransform.position, Vector3.down, out hit, 5f, grounded.DetectableLayers)) {
                timer = 0.5f;
            } else {
                timer -= TimeValues.Delta;
                if (timer <= 0) {
                    stateMachine.ChangeState(stateMachine.swingState);
                }
            }
        }
    }
}