using UnityEngine;

namespace LMO {

    public class PlayerDiveState : PlayerBaseState {

        PlayerDive dive;
        PlayerInput input;
        Grounded grounded;

        public PlayerDiveState(PlayerController playerController) : base(playerController) {
            dive = context.playerDive;
            input = context.playerInput;
            grounded = playerController.playerJump.groundedSystem;
        }

        public override void OnStateEnter() {
            Grounded.OnLanded += MoveToIdleState;
            dive.OnHitObject += MoveToIdleState;

            dive.StartDive();
        }

        public override void OnStateExit() {
            Grounded.OnLanded -= MoveToIdleState;
            dive.OnHitObject -= MoveToIdleState;
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
    }
}