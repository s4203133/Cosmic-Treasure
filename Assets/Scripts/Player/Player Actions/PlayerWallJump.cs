using System;
using UnityEngine;

namespace LMO {

    public class PlayerWallJump : MonoBehaviour {

        [SerializeField] private LayerMask detectableLayers;
        [SerializeField] private float wallDetectionDistance;
        [SerializeField] private float angleIntoWallThreshold;

        private GameObject player;

        private CameraDirection playerCamera;
        private PlayerInput playerInput;

        private Vector3 wallSurface;
        public Vector3 jumpDirection { get; private set; }
        private bool isByWall;
        public bool WallInFrontOfPlayer => isByWall;

        public static Action OnWallSlideStart;
        public static Action OnWallSlideEnd;
        public static Action OnWallJump;

        private void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
            isByWall = false;

            playerCamera = new CameraDirection(Camera.main.transform);
            playerInput = player.GetComponent<PlayerInput>();
        }

        public void CheckForWalls() {
            Vector3 moveDirection = PlayerMoveDirection();
            if (Physics.Raycast(player.transform.position, moveDirection, out RaycastHit hit, wallDetectionDistance, detectableLayers)) {
                if (Vector3.Dot(moveDirection, hit.normal) <= angleIntoWallThreshold) {
                    isByWall = true;
                    wallSurface = hit.normal;
                    return;
                }
            }
            isByWall = false;
        }

        private Vector3 PlayerMoveDirection() {
            playerCamera.CalculateDirection();
            Vector3 forwardMovement = playerCamera.Forward * playerInput.moveInput.y;
            Vector3 rightMovement = playerCamera.Right * playerInput.moveInput.x;
            return (forwardMovement + rightMovement).normalized;
        }

        public Vector3 PlayerFaceWallDirection() {
            return player.transform.position - wallSurface;
        }

        public Vector3 GetKickBackLookPoint() {
            return player.transform.position + wallSurface;
        }

        public void CalculateJumpDirection() {
            jumpDirection = wallSurface;
        }
    }
}