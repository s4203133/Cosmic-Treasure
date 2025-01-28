using UnityEngine;

public class PlayerGroundPoundState : PlayerBaseState {

    private PlayerGroundPound groundPound;
    private PlayerJump jump;
    private bool playedLandingVFX;

    public PlayerGroundPoundState(PlayerController playerController) : base(playerController) {
        groundPound = context.playerGroundPound;
        jump = context.playerJump;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    public override void OnStateEnter() {
        InputHandler.jumpStarted += CheckJumpInput;
        playedLandingVFX = false;

        groundPound.StartGroundPound();
    }

    public override void OnStateExit() {
        InputHandler.jumpStarted -= CheckJumpInput;
    }

    public override void OnStatePhysicsUpdate() {
        groundPound.ApplyGroundPoundForce();
    }

    public override void OnStateUpdate() {
        CheckLanded();
        CheckFinished();
    }

    private void CheckFinished() {
        if (groundPound.finishedGroundPound) {
            MoveToIdleState();
        }
    }

    private void CheckLanded() {
        if (groundPound.landed) {
            if(!playedLandingVFX) {
                context.vfx.PlayGroundPoundParticles();
                context.squashAndStretch.GroundPound.Play();
                playedLandingVFX = true;
            }
        }
    }

    private void CheckJumpInput() {
        if (groundPound.landed) {
            stateMachine.ChangeState(stateMachine.highJumpState);
        }
    }

    private void MoveToIdleState() {
        stateMachine.ChangeState(stateMachine.idleState);
    }
}
