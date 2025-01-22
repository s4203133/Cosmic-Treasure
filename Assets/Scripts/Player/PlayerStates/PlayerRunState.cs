using UnityEngine;

[System.Serializable]
public class PlayerRunState : PlayerBaseState {

    private PlayerMovement movement;

    public PlayerRunState(PlayerStateMachine playerStateMachine, PlayerMovement movement) : base(playerStateMachine) {
        this.movement = movement;
    }

    public override void OnStateEnter() {
        InputHandler.moveCancelled += StopMovement;
        InputHandler.jumpStarted += Jump;
    }

    public override void OnStateUpdate() {
        
    }

    public override void OnStatePhysicsUpdate() {
        movement.MoveCharacter();
    }

    public override void OnStateExit() {
        InputHandler.moveCancelled -= StopMovement;
        InputHandler.jumpStarted -= Jump;
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void StopMovement(Vector2 inputValue) {
        movement.StopMovement();
        stateMachine.ChangeState(stateMachine.idleState);
    }

    private void Jump() {
        stateMachine.ChangeState(stateMachine.jumpState);
    }
}
