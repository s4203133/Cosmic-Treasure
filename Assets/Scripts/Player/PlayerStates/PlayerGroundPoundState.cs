using UnityEngine;

public class PlayerGroundPoundState : PlayerBaseState {

    public delegate void CustomEvent();
    public static CustomEvent OnLanded;

    private PlayerGroundPound groundPound;
    private bool playedLandingVFX;

    public PlayerGroundPoundState(PlayerController playerController) : base(playerController) {
        groundPound = context.playerGroundPound;
    }

    public override void OnCollisionEnter(Collision collision) {
    }

    public override void OnStateEnter() {
        InputHandler.jumpStarted += CheckJumpInput;
        OnLanded += PlayLandEffects;

        playedLandingVFX = false;

        groundPound.StartGroundPound();
    }

    public override void OnStateExit() {
        InputHandler.jumpStarted -= CheckJumpInput;
        OnLanded -= PlayLandEffects;
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
                OnLanded?.Invoke();
            }
        }
    }

    private void PlayLandEffects() {
        context.vfx.PlayGroundPoundParticles();
        context.squashAndStretch.GroundPound.Play();
        playedLandingVFX = true;
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
