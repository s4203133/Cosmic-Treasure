using UnityEngine;

namespace LMO {

    public class PlayerGrappleConnectedIdle : PlayerIdleState {

        private SwingManager swingManager;
        private Transform thisTransform;
        private Grapple grapple;

        public PlayerGrappleConnectedIdle(PlayerController playerController) : base(playerController) {
            swingManager = context.playerSwingManager;
            thisTransform = playerController.transform;
            grapple = playerController.playerGrapple;
        }

        public override void OnStateUpdate() {
            Vector3 targetDirection = swingManager.SwingTarget.transform.position;
            targetDirection.y = thisTransform.position.y;
            thisTransform.LookAt(targetDirection);
        }

        protected override void StartedMoving(Vector2 value) {
            if (value != Vector2.zero) {
                stateMachine.ChangeState(stateMachine.grappleRun);
            }
        }

        protected override void Jump() {
            if (stateMachine.controller.playerJump.CanJump()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        protected override void CheckForJumpInput() {
            if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        protected override void Spin() {
            grapple.OnGrappleEnded?.Invoke();
            stateMachine.ChangeState(stateMachine.spinState);
        }

        protected override void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }

        protected override void Grapple() {

        }
    }
}