using UnityEngine;

namespace LMO {

    public class PlayerIdleState : PlayerBaseState {

        private PlayerIdle idle;

        public PlayerIdleState(PlayerController playerController) : base(playerController) {
            idle = context.playerIdle;
        }

        public override void OnStateEnter() {
            InputHandler.moveStarted += StartedMoving;
            InputHandler.movePerformed += StartedMoving;
            InputHandler.jumpStarted += Jump;
            InputHandler.SpinStarted += Spin;
            InputHandler.grappleStarted += Grapple;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            CheckForJumpInput();
        }

        public override void OnStateUpdate() {

        }

        public override void OnStatePhysicsUpdate() {
            idle.Idle();
        }

        public override void OnStateExit() {
            InputHandler.moveStarted -= StartedMoving;
            InputHandler.movePerformed -= StartedMoving;
            InputHandler.jumpStarted -= Jump;
            InputHandler.SpinStarted -= Spin;
            InputHandler.grappleStarted -= Grapple;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;
        }

        public override void OnTriggerEnter(Collider other) {

        }

        // Transition to state based off input 

        protected virtual void StartedMoving(Vector2 value) {
            if (value != Vector2.zero) {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }

        protected virtual void Jump() {
            if (stateMachine.controller.playerJump.CanJump()) {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
        }

        protected virtual void CheckForJumpInput() {
            if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
                stateMachine.ChangeState(stateMachine.jumpState);
            }
        }

        protected virtual void Spin() {
            stateMachine.ChangeState(stateMachine.spinState);
        }

        protected virtual void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }

        protected virtual void Grapple() {
            stateMachine.GrappleToTarget();
        }
    }
}