using Cinemachine;
using UnityEngine;

namespace LMO {

    public class CameraSwapper : MonoBehaviour {
        public bool canChange;
        [SerializeField] private CinemachineVirtualCamera targetCamera;
        [SerializeField] private int newPriority;
        private int previousPriority;

        [SerializeField] private CameraShaker cameraShaker;

        private void Start() {
            previousPriority = targetCamera.Priority;
            canChange = true;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                ActivateCamera();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                DeativateCamera();

            }
        }

        public void ActivateCamera() {
            if(!canChange) {
                return;
            }
            targetCamera.Priority = newPriority;
            if (cameraShaker != null) {
                cameraShaker.RegisterAlternateCamera(targetCamera);
            }
        }

        public void DeativateCamera() {
            targetCamera.Priority = previousPriority;
            if (cameraShaker != null) {
                cameraShaker.UnRegisterAlternateCamera();
            }
        }
    }
}