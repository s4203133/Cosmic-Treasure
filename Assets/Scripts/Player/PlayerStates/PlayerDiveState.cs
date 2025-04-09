using UnityEngine;

namespace LMO {

    public class PlayerDiveState : PlayerBaseState {

        PlayerDive dive;
        PlayerInput input;
        Grounded grounded;
        PlayerSpinAttack spin;

        public PlayerDiveState(PlayerController playerController) : base(playerController) {
            dive = context.playerDive;
            input = context.playerInput;
            grounded = context.playerJump.groundedSystem;
            spin = context.playerSpinAttack;
        }

        public override void OnStateEnter() {
            Grounded.OnLanded += MoveToIdleState;
            PlayerDive.OnHitObject += MoveToIdleState;
            InputHandler.SpinStarted += Spin;

            dive.StartDive();
        }

        public override void OnStateExit() {
            Grounded.OnLanded -= MoveToIdleState;
            PlayerDive.OnHitObject -= MoveToIdleState;
            InputHandler.SpinStarted -= Spin;
        }

        public override void OnStateUpdate() {
            dive.Countdown();
        }

        public override void OnStatePhysicsUpdate() {
            dive.HandleDive();
        }

        public override void OnTriggerEnter(Collider collider) {

        }

        private void MoveToIdleState() {
            if (!grounded.IsOnGround) {
                stateMachine.Fall();
            }
            if (input.moveInput == Vector2.zero) {
                stateMachine.Idle();
            }
            else {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }

        private void Spin() {
            if (!spin.CanAirSpin) {
                stateMachine.ChangeState(stateMachine.spinState);
            }
        }
    }
}