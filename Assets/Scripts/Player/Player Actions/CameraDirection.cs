using UnityEngine;

namespace LMO {

    public class CameraDirection {
        Transform camTransform;
        private Vector3 forward;
        private Vector3 right;

        public Vector3 Forward => forward;
        public Vector3 Right => right;

        public CameraDirection(Transform targetTransform) {
            camTransform = targetTransform;
        }

        public void CalculateDirection() {
            forward = camTransform.forward;
            right = camTransform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();
        }
    }
}