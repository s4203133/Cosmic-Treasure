using System;
using UnityEngine;

namespace LMO {

    public class Grounded : MonoBehaviour {
        private bool isGrounded;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform groundPoint;
        [SerializeField] private LayerMask jumpLayers;
        [SerializeField] private LayerMask movingPlatformLayers;
        private GameObject parentMovingPlatform;
        public LayerMask DetectableLayers => jumpLayers;
        [SerializeField] private float groundCheckRadius;

        private bool hasStartedJump;
        private bool sentGroundedEvent;
        private bool isOnMovingPlatform;

        private float notGroundedTimer;
        public float timeSinceLeftGround => notGroundedTimer;

        public static Action OnLanded;

        public bool IsOnGround => isGrounded;

        void Update() {
            CheckIsGrounded();
            CheckPlayerLeftMovingPlatform();
            EndStartOfJump();
            SendGroundedEvent();
        }

        private bool DetectingGround() {
            return Physics.CheckSphere(groundPoint.position, groundCheckRadius, jumpLayers);
        }

        private void CheckIsGrounded() {
            if (hasStartedJump) {
                isGrounded = false;
                return;
            }
            isGrounded = DetectingGround();
        }

        public void EndStartOfJump() {
            if (!DetectingGround() || !IsOnGround) {
                hasStartedJump = false;
            }
        }

        public void NotifyLeftGround() {
            hasStartedJump = true;
            isGrounded = false;
        }

        private void SendGroundedEvent() {
            if (isGrounded) {
                notGroundedTimer = 0;
                // Broadcast message that the player has landed on the ground
                if (!sentGroundedEvent) {
                    OnLanded?.Invoke();
                    sentGroundedEvent = true;
                }
            }
            else {
                notGroundedTimer += TimeValues.Delta;
                sentGroundedEvent = false;
            }
        }

        private void CheckPlayerLeftMovingPlatform() {
            if (!isGrounded) {
                if (isOnMovingPlatform) {
                    if (!MovingPlatformIsUnderneathPlayer()) {
                        RemovePlayerFromMovingPlatform();
                    }
                }
            }
        }

        public void AttatchPlayerToMovingPlatform() {
            Collider[] groundObjects = Physics.OverlapSphere(groundPoint.position, groundCheckRadius, movingPlatformLayers);
            if (groundObjects.Length > 0) {
                for (int i = 0; i < groundObjects.Length; i++) {
                    if (movingPlatformLayers == (movingPlatformLayers | (1 << groundObjects[i].gameObject.layer))) {
                        ParentPlayerToMovingPlatform(groundObjects[i].gameObject);
                        break;
                    }
                }
            }
        }

        private void ParentPlayerToMovingPlatform(GameObject platform) {
            parentMovingPlatform = platform.transform.gameObject;
            playerTransform.SetParent(platform.transform);
            isOnMovingPlatform = true;
        }

        private void RemovePlayerFromMovingPlatform() {
            parentMovingPlatform = null;
            playerTransform.SetParent(null);
            isOnMovingPlatform = false;
        }

        private bool MovingPlatformIsUnderneathPlayer() {
            RaycastHit hit;
            if (Physics.Raycast(playerTransform.position, Vector3.down, out hit, 25f, movingPlatformLayers)) {
                if (hit.collider.gameObject == parentMovingPlatform) {
                    return true;
                }
            }
            return false;
        }
    }
}