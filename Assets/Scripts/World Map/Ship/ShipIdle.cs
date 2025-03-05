using UnityEngine;

namespace LMO {

    public class ShipIdle : PlayerBaseState {

        private ShipStateMachine shipStateMachine;

        public ShipIdle(PlayerController playerController) : base(playerController) {
            shipStateMachine = playerController.playerStateMachine as ShipStateMachine;
        }

        public override void OnStateEnter() {
            InputHandler.moveStarted += StartedMoving;
            InputHandler.movePerformed += StartedMoving;
        }

        public override void OnStateExit() {
            InputHandler.moveStarted -= StartedMoving;
            InputHandler.movePerformed -= StartedMoving;
        }

        public override void OnStatePhysicsUpdate() {

        }

        public override void OnStateUpdate() {

        }

        public override void OnTriggerEnter(Collider collider) {

        }

        private void StartedMoving(Vector2 value) {
            if (value != Vector2.zero) {
                shipStateMachine.ChangeState(shipStateMachine.shipMoveState);
            }
        }
    }
}