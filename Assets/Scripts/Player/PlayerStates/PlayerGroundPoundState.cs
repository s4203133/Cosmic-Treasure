using UnityEngine;

public class PlayerGroundPoundState : PlayerBaseState {

    private PlayerGroundPound groundPound;

    public PlayerGroundPoundState(PlayerController playerController) : base(playerController) {
        groundPound = context.playerGroundPound;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    public override void OnStateEnter() {
        InputHandler.jumpStarted += CheckJumpInput;

        if (!groundPound.canGroundPound) {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }

        groundPound.StartGroundPound();
    }

    public override void OnStateExit() {
        InputHandler.jumpStarted -= CheckJumpInput;

        groundPound.FinishGroundPound();
    }

    public override void OnStatePhysicsUpdate() {
        groundPound.ApplyGroundPoundForce();
    }

    public override void OnStateUpdate() {
        CheckFinished();
    }

    private void CheckFinished() {
        if (groundPound.finishedGroundPound) {
            MoveToIdleState();
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
}
