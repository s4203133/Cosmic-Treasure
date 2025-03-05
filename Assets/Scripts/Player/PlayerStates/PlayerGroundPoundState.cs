using UnityEngine;

namespace LMO {

    public class PlayerGroundPoundState : PlayerBaseState {

        private PlayerGroundPound groundPound;
        private bool diveRegistered;

        public PlayerGroundPoundState(PlayerController playerController) : base(playerController) {
            groundPound = context.playerGroundPound;
        }

        public override void OnTriggerEnter(Collider collider) {
        }

        public override void OnStateEnter() {
            InputHandler.jumpStarted += CheckJumpInput;
            InputHandler.SpinStarted += RegisterDiveInput;
            groundPound.OnGroundPoundFinished += MoveToIdleState;

            diveRegistered = false;

            if (!groundPound.canGroundPound) {
                stateMachine.ChangeState(stateMachine.idleState);
                return;
            }

            groundPound.StartGroundPound();
        }

        public override void OnStateExit() {
            InputHandler.jumpStarted -= CheckJumpInput;
            InputHandler.SpinStarted -= RegisterDiveInput;
            groundPound.OnGroundPoundFinished -= MoveToIdleState;
        }

        public override void OnStatePhysicsUpdate() {
            // If the dive input has been registered, move to that state
            if (diveRegistered) {
                bool diving = ValidateDive();
                if (diving) {
                    return;
                }
            }
            groundPound.HandleGroundPound();
        }

        public override void OnStateUpdate() {
            // If the dive input has been registered, move to that state
            if (diveRegistered) {
                bool diving = ValidateDive();
                if (diving) {
                    return;
                }
            }
        }

        // Perform high jump if player jumps while landing a ground pound
        private void CheckJumpInput() {
            if (groundPound.hasLanded) {
                stateMachine.ChangeState(stateMachine.highJumpState);
            }
        }

        private void MoveToIdleState() {
            stateMachine.ChangeState(stateMachine.idleState);
        }

        private void RegisterDiveInput() {
            diveRegistered = true;
        }

        // Check if the player can dive and change states
        private bool ValidateDive() {
            if (groundPound.canDive) {
                groundPound.FinishGroundPound();
                Dive();
                return true;
            }
            return false;
        }

        private void Dive() {
            stateMachine.ChangeState(stateMachine.diveState);
        }
    }
}