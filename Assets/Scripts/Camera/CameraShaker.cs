using Cinemachine;
using UnityEngine;

namespace LMO {

    public class CameraShaker : MonoBehaviour {
        [Header("CAMERA SHAKE SETTINGS")]
        [SerializeField] private CinemachineFreeLook cinemachine;
        private CinemachineBasicMultiChannelPerlin[] cinemachineShakes = new CinemachineBasicMultiChannelPerlin[3];

        [SerializeField] private float maxDuration;
        [SerializeField] private float maxMagnitude;
        [SerializeField] private float magnitudeReductionRate;

        [Header("CAMERA SHAKES")]
        [SerializeField] private CameraShakes cameraShakes;
        public CameraShakes shakeTypes => cameraShakes;

        private float duration;
        private float magnitude;
        private float intensity;

        private void OnEnable() {
            GetCameras();
            SubscribeCameraShakes();
        }

        private void OnDisable() {
            UnsubscribeCameraShakes();
        }

        private void Update() {
            Shake();
            ReduceShake();
        }

        private void ShakeCamera(CameraShake shake) {
            // Increase the shake and duration, but ensure it stays within the range of 0 to 1
            intensity += shake.Magnitude;
            magnitude = Mathf.Pow(intensity, 3);
            duration += shake.Duration;

            magnitude = Mathf.Clamp(magnitude, 0f, maxMagnitude);
            duration = Mathf.Clamp(duration, 0f, maxDuration);
        }

        private void Shake() {
            // Get a new random rotation and position to set the camera
            for (int i = 0; i < 3; i++) {
                cinemachineShakes[i].m_AmplitudeGain = magnitude;
                cinemachineShakes[i].m_FrequencyGain = duration;
            }
        }

        private void GetCameras() {
            for (int i = 0; i < 3; i++) {
                cinemachineShakes[i] = cinemachine.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
        }

        private void ReduceShake() {
            // Reduce the duration and intesity of the shake across time
            duration -= Time.deltaTime;
            if (duration < 0) {
                duration = 0;
            }

            intensity -= (Time.deltaTime * magnitudeReductionRate);
            if (intensity < 0) {
                intensity = 0;
            }
            magnitude = Mathf.Pow(intensity, 2);
        }

        private void SubscribeCameraShakes() {
            cameraShakes.small.OnShake += ShakeCamera;
            cameraShakes.medium.OnShake += ShakeCamera;
            cameraShakes.large.OnShake += ShakeCamera;
        }

        private void UnsubscribeCameraShakes() {
            cameraShakes.small.OnShake -= ShakeCamera;
            cameraShakes.medium.OnShake -= ShakeCamera;
            cameraShakes.large.OnShake -= ShakeCamera;
        }
    }
}