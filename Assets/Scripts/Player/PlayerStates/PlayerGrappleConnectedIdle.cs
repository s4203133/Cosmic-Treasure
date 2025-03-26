using UnityEngine;

namespace LMO {

    public class PlayerGrappleConnectedIdle : PlayerGrappleState {

        PlayerIdle idle;

        public PlayerGrappleConnectedIdle(PlayerController playerController) : base(playerController) {
            idle = playerController.playerIdle;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();
            InputHandler.moveStarted += StartedMoving;
            InputHandler.movePerformed += StartedMoving;
            InputHandler.jumpStarted += Jump;
            InputHandler.SpinStarted += Spin;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            CheckForJumpInput();
        }

        public override void OnStateUpdate() {
            LookAtGrappleTarget();
        }

        public override void OnStatePhysicsUpdate() {
            idle.Idle();
        }

        public override void OnStateExit() {
            base.OnStateExit();
            InputHandler.moveStarted -= StartedMoving;
            InputHandler.movePerformed -= StartedMoving;
            InputHandler.jumpStarted -= Jump;
            InputHandler.SpinStarted -= Spin;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;
        }

        private void StartedMoving(Vector2 value) {
            if (value != Vector2.zero) {
                stateMachine.ChangeState(stateMachine.grappleRun);
            }
        }

        private void Jump() {
            if (stateMachine.controller.playerJump.CanJump()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        private void CheckForJumpInput() {
            if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
                stateMachine.ChangeState(stateMachine.grappleJump);
            }
        }

        private void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}