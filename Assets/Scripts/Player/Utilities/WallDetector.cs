using UnityEngine;

namespace LMO.Player {

    public class WallDetector : MonoBehaviour {
        [SerializeField] private Transform playerTransform;

        private bool wallInFront;
        private Transform thisTransform;
        [SerializeField] private LayerMask detectableLayers;

        [SerializeField] private float inputThreshold;
        private Vector2 input;
        private Vector2 directionToWall;

        [SerializeField] private PlayerMovement movement;
        private bool movingTowardsWall;
        private bool oldMovingTowardsWall;

        private float dot;

        void Awake() {
            thisTransform = transform;
            SubscribeInputEvent();
        }

        void Update() {
            DetectWall();
            GetDirectionToWall();

            TestInput();
            NotifyMovement();
        }

        private void GetInput(Vector2 value) {
            input = value;
        }

        private void SubscribeInputEvent() {
            InputHandler.moveStarted += GetInput;
            InputHandler.movePerformed += GetInput;
            InputHandler.moveCancelled += GetInput;
        }

        private void DetectWall() {
            wallInFront = Physics.CheckSphere(thisTransform.position, 0.15f, detectableLayers);
            if (wallInFront) {
                GetDirectionToWall();
            }
        }

        private void GetDirectionToWall() {
            directionToWall = new Vector2(thisTransform.position.x - playerTransform.position.x, thisTransform.position.z - playerTransform.position.z);
        }

        private void TestInput() {
            if (!wallInFront || input == Vector2.zero) {
                movingTowardsWall = false;
                return;
            }

            dot = Vector2.Dot(input, directionToWall);
            if (dot > inputThreshold) {
                movingTowardsWall = true;
            } else {
                movingTowardsWall = false;
            }
        }

        private void NotifyMovement() {
            if (movingTowardsWall != oldMovingTowardsWall) {
                oldMovingTowardsWall = movingTowardsWall;
                //movement.NotifyWallInFront(movingTowardsWall);
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;

            Vector3 dirToWall = new Vector3(directionToWall.x, 0, directionToWall.y);
            Vector3 inp = new Vector3(input.x, 0.25f, input.y);
            Vector3 startPosition = playerTransform.position + Vector3.up * 0.5f;

            Gizmos.DrawLine(startPosition, startPosition + dirToWall * 3);

            if (dot > inputThreshold) {
                Gizmos.color = Color.yellow;
            } else {
                Gizmos.color = Color.blue;
            }

            Gizmos.DrawLine(startPosition, startPosition + inp * 3);
        }
    }
}