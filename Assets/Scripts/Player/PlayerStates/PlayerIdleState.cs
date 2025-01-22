using UnityEngine;

[System.Serializable]
public class PlayerIdleState : PlayerBaseState {


    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) {
    }

    public override void OnStateEnter() {
        InputHandler.moveStarted += StartedMoving;
        InputHandler.jumpStarted += Jump;
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
        stateMachine.ChangeState(stateMachine.jumpState);
    }
}
