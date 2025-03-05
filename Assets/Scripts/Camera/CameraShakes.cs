using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class CameraShakes {
        [SerializeField] private CameraShakeType smallCameraShake;
        [SerializeField] private CameraShakeType mediumCameraShake;
        [SerializeField] private CameraShakeType largeCameraShake;

        public CameraShakeType small => smallCameraShake;
        public CameraShakeType medium => mediumCameraShake;
        public CameraShakeType large => largeCameraShake;
    }
}