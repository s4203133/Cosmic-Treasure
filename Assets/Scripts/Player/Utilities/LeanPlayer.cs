using UnityEngine;

namespace LMO {

    public class LeanPlayer : MonoBehaviour {
        [SerializeField] private float leanAmount;
        [SerializeField] private float smoothness;
        [SerializeField] private PlayerInput input;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform playerVisualTransform;
        private CameraDirection cameraDirection;
        private Transform thisTransform;

        Vector3 direction;

        void Start() {
            cameraDirection = new CameraDirection(cameraTransform);
            thisTransform = transform;
        }

        void Update() {
            GetLeanDirection();
        }

        private void GetLeanDirection() {
            //GetMoveDirection();
            //...
        }

        private void GetMoveDirection() {
            if (input.moveInput == Vector2.zero) {
                thisTransform.rotation = Quaternion.Lerp(thisTransform.localRotation, Quaternion.Euler(0, thisTransform.localRotation.y, thisTransform.localRotation.z), smoothness);
            } else {
                thisTransform.rotation = Quaternion.Lerp(thisTransform.localRotation, Quaternion.Euler(leanAmount, thisTransform.localRotation.y, thisTransform.localRotation.z), smoothness);
            }
        }
    }
}