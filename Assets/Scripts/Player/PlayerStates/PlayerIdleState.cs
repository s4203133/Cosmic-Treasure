using UnityEngine;

[System.Serializable]
public class PlayerIdleState : PlayerBaseState {


    public PlayerIdleState(PlayerController playerController) : base(playerController) {
    }

    public override void OnStateEnter() {
        InputHandler.moveStarted += StartedMoving;
        InputHandler.jumpStarted += Jump;
        CheckForJumpInput();
    }

    public override void OnStateUpdate() {

    }

    public override void OnStatePhysicsUpdate() {

    }

    public override void OnStateExit() {
        InputHandler.moveStarted -= StartedMoving;
        InputHandler.jumpStarted -= Jump;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    private void StartedMoving(Vector2 value) {
        if(value.x != 0 || value.y != 0) {
            stateMachine.ChangeState(stateMachine.runState);
        }
    }

    private void Jump() {
        if (stateMachine.controller.playerJump.CanJump()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void CheckForJumpInput() {
        if (context.inputBufferHolder.jumpInputBuffer.HasInputBeenRecieved()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }
}
