using UnityEngine;

public class PlayerGroundPoundState : PlayerBaseState {

    public delegate void CustomEvent();
    public static CustomEvent OnLanded;

    private PlayerGroundPound groundPound;

    public PlayerGroundPoundState(PlayerController playerController) : base(playerController) {
        groundPound = context.playerGroundPound;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    public override void OnStateEnter() {
        InputHandler.jumpStarted += CheckJumpInput;

        groundPound.StartGroundPound();
    }

    public override void OnStateExit() {
        InputHandler.jumpStarted -= CheckJumpInput;
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
