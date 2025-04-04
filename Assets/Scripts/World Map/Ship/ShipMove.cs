using UnityEngine;

namespace LMO {

    public class ShipMove : PlayerBaseState {

        private ShipStateMachine shipStateMachine;
        private PlayerMovement movement;
        private PlayerInput input;

        public ShipMove(PlayerController playerController) : base(playerController) {
            shipStateMachine = playerController.playerStateMachine as ShipStateMachine;
            movement = context.playerMovment;
            input = context.playerInput;
        }

        public override void OnStateEnter() {
            PlayerMovement.OnMoveStarted?.Invoke();
        }

        public override void OnStateExit() {
            PlayerMovement.OnMoveStopped?.Invoke();
        }

        public override void OnStatePhysicsUpdate() {
            movement.HandleMovement();
        }

        public override void OnStateUpdate() {
            CheckIfFinishedMoving();
        }

        public override void OnTriggerEnter(Collider collider) {

        }

        private void CheckIfFinishedMoving() {
            if (input.moveInput == Vector2.zero) {
                if (movement.HasStopped()) {
                    shipStateMachine.ChangeState(shipStateMachine.shipIdleState);
                }
            }
        }
    }
}