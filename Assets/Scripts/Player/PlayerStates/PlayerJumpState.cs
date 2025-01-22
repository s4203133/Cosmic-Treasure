using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private PlayerMovement movement;
    private PlayerJump jump;

    public PlayerJumpState(PlayerStateMachine playerStateMachine , PlayerMovement movement, PlayerJump jump) : base(playerStateMachine) {
        this.movement = movement;
        this.jump = jump;
    }

    public override void OnStateEnter() {
        InputHandler.jumpCancelled += jump.CutOffJump;
        jump.InitialiseJump();
    }

    public override void OnStateUpdate() {
        if (jump.HasLanded()) {
            if (movement.moveInput.x == 0 && movement.moveInput.y == 0) {
                stateMachine.ChangeState(stateMachine.idleState);
            } else {
                stateMachine.ChangeState(stateMachine.runState);
            }
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.MoveCharacter();
        jump.ApplyForce();
    }

    public override void OnStateExit() {
        InputHandler.jumpCancelled -= jump.CutOffJump;
        jump.EndJump();
    }

    public override void OnCollisionEnter(Collision collision) {

    }
}
