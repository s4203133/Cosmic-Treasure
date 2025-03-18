using Cinemachine;
using UnityEngine;

namespace LMO {

    public class CameraSwapper : MonoBehaviour {
        [SerializeField] private CinemachineVirtualCamera targetCamera;
        [SerializeField] private int newPriority;
        private int previousPriority;

        [SerializeField] private CameraShaker cameraShaker;

        private void Start() {
            previousPriority = targetCamera.Priority;
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
            targetCamera.Priority = newPriority;
            cameraShaker.RegisterAlternateCamera(targetCamera);
        }

        public void DeativateCamera() {
            targetCamera.Priority = previousPriority;
            cameraShaker.UnRegisterAlternateCamera();
        }
    }
}