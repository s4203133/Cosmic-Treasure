using UnityEngine;

[System.Serializable]
public class PlayerIdleState : PlayerBaseState {

    private PlayerIdle idle;

    public PlayerIdleState(PlayerController playerController) : base(playerController) {
        idle = context.playerIdle;
    }

    public override void OnStateEnter() {
        InputHandler.moveStarted += StartedMoving;
        InputHandler.movePerformed += StartedMoving;
        InputHandler.jumpStarted += Jump;
        InputHandler.SpinStarted += Spin;

        CheckForJumpInput();
    }

    public override void OnStateUpdate() {

    }

    public override void OnStatePhysicsUpdate() {
        idle.Idle();
    }

    public override void OnStateExit() {
        InputHandler.moveStarted -= StartedMoving;
        InputHandler.movePerformed -= StartedMoving;
        InputHandler.jumpStarted -= Jump;
        InputHandler.SpinStarted -= Spin;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    private void StartedMoving(Vector2 value) {
        if(value != Vector2.zero) {
            stateMachine.ChangeState(stateMachine.runState);
        }
    }

    private void Jump() {
        if (stateMachine.controller.playerJump.CanJump()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void CheckForJumpInput() {
        if (InputBuffers.instance.jump.HasInputBeenRecieved()) {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void Spin() {
        stateMachine.ChangeState(stateMachine.spinState);
    }
}
