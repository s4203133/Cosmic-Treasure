using UnityEngine;

namespace LMO {

    public class PlayerWallJumpState : PlayerJumpState {

        Transform thisTransform;
        Rigidbody rigidBody;
        PlayerWallJump wallJump;

        PlayerMovementSettings wallJumpSettings;
        PlayerMovementSettings originalMovementSettings;

        public PlayerWallJumpState(PlayerController playerController) : base(playerController) {
            thisTransform = context.transform;
            rigidBody = context.RigidBody;
            wallJump = context.playerWallJump;

            wallJumpSettings = context.PlayerSettings.WallJumpMovement;
            originalMovementSettings = context.PlayerSettings.Movement;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();

            movement.ChangeMovementSettings(wallJumpSettings);
        }

        public override void OnStatePhysicsUpdate() {
            jump.ApplyForce();
            HandleMoveDirection();
            thisTransform.LookAt(wallJump.GetKickBackLookPoint());
        }

        private void HandleMoveDirection() {
            Vector3 moveDirection = wallJump.jumpDirection.normalized;
            moveDirection *= movement.CurrentMovementSettings.MaxSpeed * TimeValues.FixedDelta;
            moveDirection.y = rigidBody.velocity.y;
            rigidBody.velocity = moveDirection;
        }

        public override void OnStateExit() {
            movement.ChangeMovementSettings(originalMovementSettings);

            base.OnStateExit();
        }
    }
}