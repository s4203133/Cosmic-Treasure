
namespace LMO {
    public class PlayerSmallSpringJump : PlayerJumpState {

        public PlayerSmallSpringJump(PlayerController playerController) : base(playerController) {
            jumpSettings = context.PlayerSettings.SmallSpringJump;
        }

        public override void OnStateEnter() {
            InputHandler.SpinStarted += Spin;
            InputHandler.jumpStarted += Hover;
            InputHandler.groundPoundStarted += GroundPound;
            InputHandler.grappleStarted += Grapple;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            StartJump();
        }

        public override void OnStateExit() {
            InputHandler.SpinStarted -= Spin;
            InputHandler.jumpStarted -= Hover;
            InputHandler.groundPoundStarted -= GroundPound;
            InputHandler.grappleStarted -= Grapple;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            jump.EndJump();
        }

        private void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}