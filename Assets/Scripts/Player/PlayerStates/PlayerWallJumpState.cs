using UnityEngine;

namespace LMO {

    public class PlayerWallJumpState : PlayerJumpState {

        Transform thisTransform;
        Rigidbody rigidBody;
        PlayerWallJump wallJump;

        PlayerMovementSettings wallJumpSettings;
        PlayerMovementSettings originalMovementSettings;

        private float preventHoverTime;
        private float timer;
        private bool pressingHover;

        public PlayerWallJumpState(PlayerController playerController) : base(playerController) {
            thisTransform = context.transform;
            rigidBody = context.RigidBody;
            wallJump = context.playerWallJump;

            wallJumpSettings = context.PlayerSettings.WallJumpMovement;
            originalMovementSettings = context.PlayerSettings.Movement;
            preventHoverTime = 0.5f;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();
            InputHandler.jumpStarted += RegisterHoverInput;
            InputHandler.jumpCancelled += UnregisterHoverInput;

            PlayerWallJump.OnWallJump?.Invoke();
            movement.ChangeMovementSettings(wallJumpSettings);
            timer = preventHoverTime;
        }

        public override void OnStatePhysicsUpdate() {
            jump.ApplyForce();
            HandleMoveDirection();
            thisTransform.LookAt(wallJump.GetKickBackLookPoint());
        }

        public override void OnStateUpdate() {
            base.OnStateUpdate();
            timer -= TimeValues.Delta;
            if(timer <= 0 && pressingHover) {
                Hover();
            }
        }

        private void HandleMoveDirection() {
            Vector3 moveDirection = wallJump.jumpDirection.normalized;
            moveDirection *= movement.CurrentMovementSettings.MaxSpeed * TimeValues.FixedDelta;
            moveDirection.y = rigidBody.velocity.y;
            rigidBody.velocity = moveDirection;
        }

        public override void OnStateExit() {
            movement.ChangeMovementSettings(originalMovementSettings);
            InputHandler.jumpStarted -= RegisterHoverInput;
            InputHandler.jumpCancelled -= UnregisterHoverInput;

            UnregisterHoverInput();
            base.OnStateExit();
        }

        protected override void Hover() {
            if (timer <= 0) {
                base.Hover();
            }
        }

        private void RegisterHoverInput() {
            pressingHover = true;    
        }

        private void UnregisterHoverInput() {
            pressingHover = false;
        }
    }
}