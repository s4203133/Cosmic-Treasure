using UnityEngine;

namespace LMO {

    public class PlayerWallJump : MonoBehaviour {

        [SerializeField] private LayerMask detectableLayers;
        [SerializeField] private float angleIntoWallThreshold;

        private GameObject player;

        private CameraDirection playerCamera;
        private PlayerInput playerInput;

        private Vector3 wallSurface;
        private bool isByWall;
        public bool WallInFrontOfPlayer => isByWall;

        private void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
            isByWall = false;

            playerCamera = new CameraDirection(Camera.main.transform);
            playerInput = player.GetComponent<PlayerInput>();
        }

        public void CheckForWalls() {
            Vector3 moveDirection = PlayerMoveDirection();
            if (Physics.Raycast(player.transform.position, moveDirection, out RaycastHit hit, 2, detectableLayers)) {
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
            return -wallSurface;
        }

        public Vector3 GetKickBackDirection() {
            Vector3 targetDirection = PlayerMoveDirection();
            return targetDirection;
        }
    }
}