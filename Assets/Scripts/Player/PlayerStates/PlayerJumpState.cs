using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private PlayerMovement movement;
    private PlayerJump jump;

    public PlayerJumpState(PlayerStateMachine playerStateMachine, PlayerController playerController, PlayerMovement movement, PlayerJump jump) : base(playerStateMachine, playerController) {
        this.movement = movement;
        this.jump = jump;
    }

    public override void OnStateEnter() {
        jump.ResetJumpForce();
        jump.ApplyJumpForce();
    }

    public override void OnStateUpdate() {
        if (jump.isGrounded) {
            if (movement.moveDirection.x == 0 && movement.moveDirection.y == 0) {
                controller.Idle();
            } else {
                controller.Move();
            }
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.MoveCharacter();
    }

    public override void OnStateExit() {
    }

    public override void OnCollisionEnter(Collision collision) {

    }
}
