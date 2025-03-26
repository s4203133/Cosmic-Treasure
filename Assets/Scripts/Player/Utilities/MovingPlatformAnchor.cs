using UnityEngine;

namespace LMO {

    public class MovingPlatformAnchor : MonoBehaviour {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Grounded grounded;
        [SerializeField] private Transform groundPoint;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask movingPlatformLayers;

        private bool isOnMovingPlatform;
        private bool hasLeftMovingPlatform;

        private void OnEnable() {
            Grounded.OnLanded += AttachPlayerToMovingPlatform;
            Grounded.OnLeftGround += JumpFromPlatform;
        }

        private void OnDisable() {
            Grounded.OnLanded -= AttachPlayerToMovingPlatform;
            Grounded.OnLeftGround -= JumpFromPlatform;
        }

        private void Update() {
            CheckPlayerLeftMovingPlatform();
        }

        private void CheckPlayerLeftMovingPlatform() {
            if (!grounded.IsOnGround) {
                DettachPlayerFromMovingPlatform();
            }
        }

        public void AttachPlayerToMovingPlatform() {
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
            playerTransform.SetParent(platform.transform);
            isOnMovingPlatform = true;
            hasLeftMovingPlatform = false;
        }

        private void RemovePlayerFromMovingPlatform() {
            playerTransform.SetParent(null);
            isOnMovingPlatform = false;
            hasLeftMovingPlatform = false;
        }

        private void JumpFromPlatform() {
            if (isOnMovingPlatform) {
                hasLeftMovingPlatform = true;
            }
        }

        private void DettachPlayerFromMovingPlatform() {
            if (hasLeftMovingPlatform) {
                RaycastHit hit;
                if (Physics.Raycast(playerTransform.position + Vector3.up, -transform.up, out hit, 25f, movingPlatformLayers)) {
                    if (!isOnMovingPlatform) {
                        ParentPlayerToMovingPlatform(hit.collider.gameObject);
                    }
                    return;
                }
                RemovePlayerFromMovingPlatform();
            }
        }
    }
}
