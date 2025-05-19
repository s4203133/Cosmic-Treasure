using UnityEngine;
using LMO;

namespace NR {
    /// <summary>
    /// Player component that detects when a falling platform is stepped on and activates its behaviour.
    /// </summary>
    public class FallingPlatformDetector : MonoBehaviour {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Grounded grounded;
        [SerializeField] private Transform groundPoint;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask fallingPlatformLayers;

        private void OnEnable() {
            Grounded.OnLanded += ActivateFallingPlatform;
        }

        private void OnDisable() {
            Grounded.OnLanded -= ActivateFallingPlatform;
        }
        
        public void ActivateFallingPlatform() {
            Collider[] groundObjects = Physics.OverlapSphere(groundPoint.position, groundCheckRadius, fallingPlatformLayers);
            if (groundObjects.Length > 0) {
                for (int i = 0; i < groundObjects.Length; i++) {
                    if (fallingPlatformLayers == (fallingPlatformLayers | (1 << groundObjects[i].gameObject.layer))) {
                        groundObjects[i].GetComponentInParent<FallingPlatform>().Fall();
                        break;
                    }
                }
            }
        }
    }
}


