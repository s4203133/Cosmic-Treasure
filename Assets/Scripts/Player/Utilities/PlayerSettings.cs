using UnityEngine;

namespace LMO {

    public class PlayerSettings : MonoBehaviour {

        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private PlayerMovementSettings movementSettings;
        public PlayerMovementSettings Movement => movementSettings;

        [SerializeField] private PlayerMovementSettings spinMovement;
        public PlayerMovementSettings SpinAttack => spinMovement;

        [SerializeField] private PlayerMovementSettings hoverMoveSettings;
        public PlayerMovementSettings HoverMovement => hoverMoveSettings;

        [SerializeField] private PlayerMovementSettings wallJumpMoveSettings;
        public PlayerMovementSettings WallJumpMovement => wallJumpMoveSettings;


        [Header("JUMP SETTINGS")]
        [SerializeField] private PlayerJumpSettings jumpSettings;
        public PlayerJumpSettings Jump => jumpSettings;

        [SerializeField] private PlayerJumpSettings smallSpringJumpSettings;
        public PlayerJumpSettings SmallSpringJump => smallSpringJumpSettings;

        [SerializeField] private PlayerJumpSettings largeSpringJumpSettings;
        public PlayerJumpSettings LargeSpringJump => largeSpringJumpSettings;

        [SerializeField] private PlayerJumpSettings slideDownWallSettings;
        public PlayerJumpSettings SlideDownWall => slideDownWallSettings;

        [Header("GRAPPLE SETTINGS")]
        [SerializeField] private PlayerMovementSettings grappleMovementSettings;
        public PlayerMovementSettings GrappleMoveSettings => grappleMovementSettings;

    }
}