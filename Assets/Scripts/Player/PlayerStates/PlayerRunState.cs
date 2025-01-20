using UnityEngine;

[System.Serializable]
public class PlayerRunState : PlayerBaseState {

    private PlayerMovement movement;

    public PlayerRunState(PlayerStateMachine playerStateMachine, PlayerController playerController, PlayerMovement movement) : base(playerStateMachine, playerController) {
        this.movement = movement;
    }

    public override void OnStateEnter() {
        InputHandler.jumpStarted += controller.Jump;
    }

    public override void OnStateUpdate() {
        if(movement.moveDirection.x == 0 && movement.moveDirection.y == 0) {
            StopMovement();
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.MoveCharacter();
    }

    public override void OnStateExit() {
        InputHandler.jumpStarted -= controller.Jump;
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void StopMovement() {
        movement.StopMovement();
        controller.Idle();
    }
}
