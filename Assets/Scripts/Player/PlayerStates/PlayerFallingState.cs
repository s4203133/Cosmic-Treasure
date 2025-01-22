using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private PlayerMovement movement;
    private PlayerJump jump;
    private Grounded grounded;

    public PlayerFallingState(PlayerController playerController) : base(playerController) {
        movement = context.playerMovment;
        jump = context.playerJump;
        grounded = jump.groundedSystem;
    }

    public override void OnStateEnter() {

    }

    public override void OnStateUpdate() {
        if (grounded.IsOnGround()) {
            if(movement.moveInput == Vector2.zero) {
                stateMachine.ChangeState(stateMachine.idleState);
            } else {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.MoveCharacter();
        jump.ApplyFallForce();
    }

    public override void OnStateExit() {
        jump.EndJump();
    }

    public override void OnCollisionEnter(Collision collision) {

    }
}
