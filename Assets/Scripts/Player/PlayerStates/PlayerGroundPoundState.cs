using UnityEngine;

public class PlayerGroundPoundState : PlayerBaseState {

    private PlayerGroundPound groundPound;
    private bool diveRegistered;

    public PlayerGroundPoundState(PlayerController playerController) : base(playerController) {
        groundPound = context.playerGroundPound;
    }

    public override void OnCollisionEnter(Collision collision) {
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
        if (diveRegistered) {
            bool diving = ValidateDive();
            if (diving) {
                return;
            }
        }
        groundPound.HandleGroundPound();
    }

    public override void OnStateUpdate() {
        if (diveRegistered) {
            bool diving = ValidateDive();
            if (diving) {
                return;
            }
        }
    }

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
