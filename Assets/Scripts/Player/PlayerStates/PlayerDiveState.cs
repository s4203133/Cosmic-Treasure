using UnityEngine;

namespace LMO.Player {

    public class PlayerDiveState : PlayerBaseState {

        PlayerDive dive;
        PlayerInput input;

        public PlayerDiveState(PlayerController playerController) : base(playerController) {
            dive = context.playerDive;
            input = context.playerInput;
        }

        public override void OnStateEnter() {
            Grounded.OnLanded += MoveToIdleState;

            dive.StartDive();
        }

        public override void OnStateExit() {
            Grounded.OnLanded -= MoveToIdleState;
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
            if (input.moveInput == Vector2.zero) {
                stateMachine.ChangeState(stateMachine.idleState);
            } else {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }
    }
}