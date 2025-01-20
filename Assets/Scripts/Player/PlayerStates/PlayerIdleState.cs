using UnityEngine;

[System.Serializable]
public class PlayerIdleState : PlayerBaseState {

    public PlayerIdleState(PlayerStateMachine playerStateMachine, PlayerController playerController) : base(playerStateMachine, playerController) {
    }

    public override void OnStateEnter() {
        InputHandler.moveStarted += StartedMoving;
        InputHandler.jumpStarted += controller.Jump;
    }

    public override void OnStateUpdate() {
        Debug.Log("Idle");
    }

    public override void OnStatePhysicsUpdate() {

    }

    public override void OnStateExit() {
        InputHandler.moveStarted -= StartedMoving;
        InputHandler.jumpStarted -= controller.Jump;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    private void StartedMoving(Vector2 value) {
        if(value.x != 0 || value.y != 0) {
            controller.Move();
        }
    }
}
