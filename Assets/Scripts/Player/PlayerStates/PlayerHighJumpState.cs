
public class PlayerHighJumpState : PlayerJumpState {

    public PlayerHighJumpState(PlayerController playerController) : base(playerController) {

    }

    protected override void StartJump() {
        context.squashAndStretch.HighJump.Play();
        jump.InitialiseHighJump();
    }
}
