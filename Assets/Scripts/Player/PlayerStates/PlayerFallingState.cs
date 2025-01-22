using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private PlayerJump jump;
    private PlayerMovement movement;

    public PlayerFallingState(PlayerStateMachine playerStateMachine, PlayerMovement movement, PlayerJump jump) : base(playerStateMachine) {
        this.movement = movement;
        this.jump = jump;
    }

    public override void OnStateEnter() {

    }

    public override void OnStateUpdate() {
        
    }

    public override void OnStatePhysicsUpdate() {
        movement.MoveCharacter();
    }

    public override void OnStateExit() {

    }

    public override void OnCollisionEnter(Collision collision) {

    }
}
