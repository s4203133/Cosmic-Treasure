
namespace LMO {

    public class PlayerLargeSpringJump : PlayerJumpState {

        private float delay;
        private float timer;
        private bool jumped;

        public PlayerLargeSpringJump(PlayerController playerController) : base(playerController) {
            jumpSettings = context.PlayerSettings.LargeSpringJump;
            delay = 0.125f;
        }

        public override void OnStateEnter() {
            InputHandler.SpinStarted += Spin;
            InputHandler.jumpStarted += Hover;
            InputHandler.grappleStarted += Grapple;
            SpringPad.OnSmallSpringJump += SmallSpringJump;

            timer = delay;
            jumped = false;
        }

        public override void OnStateUpdate() {
            if (jumped) {
                return;
            }

            timer -= TimeValues.Delta;
            if(timer <= 0 ) { 
                StartJump();
                jumped = true;
            }
        }

        public override void OnStateExit() {
            InputHandler.SpinStarted -= Spin;
            InputHandler.jumpStarted -= Hover;
            InputHandler.grappleStarted -= Grapple;
            SpringPad.OnSmallSpringJump -= SmallSpringJump;

            jump.EndJump();
        }

        private void SmallSpringJump() {
            stateMachine.ChangeState(stateMachine.smallSpringJumpState);
        }
    }
}