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

        groundPound.FinishGroundPound();
    }

    public override void OnStatePhysicsUpdate() {
        if (diveRegistered) {
            ValidateDive();
        }

        groundPound.ApplyGroundPoundForce();
    }

    public override void OnStateUpdate() {
        if (diveRegistered) {
            ValidateDive();
            return;
        }

        CheckFinished();
    }

    private void CheckFinished() {
        if (groundPound.finishedGroundPound) {
            MoveToIdleState();
            return;
        }
    }

    private void CheckJumpInput() {
        if (groundPound.landed) {
            stateMachine.ChangeState(stateMachine.highJumpState);
        }
    }

    private void MoveToIdleState() {
        stateMachine.ChangeState(stateMachine.idleState);
    }

    private void RegisterDiveInput() {
        diveRegistered = true;
    }

    private void ValidateDive() {
/*        if (groundPound.PerformingGroundPound && !groundPound.landed) {
            groundPound.ResetGroundPound();
            Dive();
        }*/

        if (!groundPound.landed) {
            groundPound.ResetGroundPound();
            Dive();
        }
    }

    private void Dive() {
        stateMachine.ChangeState(stateMachine.diveState);
    }
}
