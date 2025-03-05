using Cinemachine;
using UnityEngine;

namespace LMO {

    public class FOVChanger : MonoBehaviour {
        private CinemachineFreeLook cam;
        private float originalFOV;
        [SerializeField] private float newFOV;
        private float targetFOV;

        [SerializeField] private float changeSpeed;
        [SerializeField] private float returnSpeed;
        private float speed;

        private bool returning;

        void Start() {
            cam = Camera.main.GetComponent<CinemachineFreeLook>();
            originalFOV = cam.m_Lens.FieldOfView;
            targetFOV = originalFOV;
            speed = changeSpeed;
        }

        void FixedUpdate() {
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, targetFOV, speed);
            CheckFinished();
        }

        public void StartChange() {
            enabled = true;
            targetFOV = newFOV;
            returning = false;
            speed = changeSpeed;
        }

        public void EndChange() {
            if (enabled) {
                targetFOV = originalFOV;
                speed = returnSpeed;
                returning = true;
            }
        }

        private void CheckFinished() {
            if (!returning) {
                return;
            }
            if (cam.m_Lens.FieldOfView < (cam.m_Lens.FieldOfView + Mathf.Epsilon)) {
                cam.m_Lens.FieldOfView = originalFOV;
                enabled = false;
            }
        }
    }
}