using UnityEngine;

public class PlayerDiveState : PlayerBaseState {

    PlayerDive dive;
    Grounded grounded;
    PlayerMovement movement;

    public PlayerDiveState(PlayerController playerController) : base(playerController) {
        dive = context.playerDive;
        grounded = context.playerJump.groundedSystem;
        movement = context.playerMovment;
    }

    public override void OnStateEnter() {
        grounded.OnLanded += MoveToIdleState;

        dive.StartDive();
    }

    public override void OnStateExit() {
        grounded.OnLanded -= MoveToIdleState;
    }

    public override void OnStateUpdate() {
        dive.Countdown();
    }

    public override void OnStatePhysicsUpdate() {
        dive.HandleDive();
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void MoveToIdleState() {
        if (movement.moveInput == Vector2.zero) {
            stateMachine.ChangeState(stateMachine.idleState);
        } else {
            stateMachine.ChangeState(stateMachine.runState);
        }
    }
}
