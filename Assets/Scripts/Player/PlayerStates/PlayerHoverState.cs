using UnityEngine;

public class PlayerHoverState : PlayerBaseState {

    PlayerHover hover;
    PlayerMovement movement;

    PlayerMovementSettings moveSettings;

    public PlayerHoverState(PlayerController playerController) : base(playerController) {
        hover = context.playerHover;
        movement = context.playerMovment;
        moveSettings = hover.movementSettings;
    }

    public override void OnStateEnter() {
        if (!hover.CheckCanHover) {
            stateMachine.ChangeState(stateMachine.fallingState);
            return;
        }

        InputHandler.jumpCancelled += hover.CuttOffHover;
        InputHandler.groundPoundStarted += GroundPound;

        movement.ChangeMovementSettings(moveSettings);
        hover.StartHover();
    }

    public override void OnStateUpdate() {
        if(hover.finished) {
            stateMachine.ChangeState(stateMachine.fallingState);
        }
    }

    public override void OnStatePhysicsUpdate() {
        movement.HandleMovement();
        hover.ApplyHoverForce();
    }

    public override void OnStateExit() {
        InputHandler.groundPoundStarted -= GroundPound;
        InputHandler.jumpCancelled -= hover.CuttOffHover;

        hover.EndHover();
    }

    public override void OnCollisionEnter(Collision collision) {

    }

    private void GroundPound() {
        hover.CuttOffHover();
        stateMachine.ChangeState(stateMachine.groundPoundState);
    }
}
