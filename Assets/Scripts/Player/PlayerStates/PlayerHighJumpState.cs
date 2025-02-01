
namespace LMO.Player {

    public class PlayerHighJumpState : PlayerJumpState {

        public PlayerHighJumpState(PlayerController playerController) : base(playerController) {

        }

        protected override void StartJump() {
            jump.InitialiseHighJump();
        }
    }
}