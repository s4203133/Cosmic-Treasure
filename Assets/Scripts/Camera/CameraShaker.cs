using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform cameraParent;

    [SerializeField] private float maxYaw;
    [SerializeField] private float maxPitch;
    [SerializeField] private float maxRoll;
    [SerializeField] private float maxPositionOffset;

    [Header("CAMERA SHAKES")]
    [SerializeField] private CameraShakes cameraShakes;
    public CameraShakes shakeTypes => cameraShakes;

    private float duration;
    private float magnitude;
    private float intensity;

    private void OnEnable() {
        SubscribeCameraShakes();
    }

    private void OnDisable() {
        UnsubscribeCameraShakes();
    }

    private void Update() {
        ReduceShake();
        Shake();
    }

    private void ShakeCamera(CameraShake shake) {
        // Increase the shake and duration, but ensure it stays within the range of 0 to 1
        duration += shake.Duration;
        intensity += shake.Magnitude;
        duration = Mathf.Clamp(duration, 0.0f, 1.0f);
        intensity = Mathf.Clamp(intensity, 0.0f, 1.0f);
    }

    private void Shake() {
        // Get a new random rotation and position to set the camera
        magnitude = Mathf.Pow(intensity, 2);

        float seed = Time.time;
        Vector3 newPosition = GetTranslationalShake(seed);
        Quaternion newRotation = GetRotationalShake(seed);

        ApplyCameraShake(newPosition, newRotation);
    }

    private void ReduceShake() {
        // Reduce the duration and intesity of the shake across time
        duration -= Time.deltaTime;
        if(duration < 0) { 
            duration = 0;
            ResetCamera();
        }

        intensity -= Time.deltaTime;
        if(intensity < 0) {
            intensity = 0; 
        }
    }

    // Return a random position for the camera
    private Vector3 GetTranslationalShake(float seed) {
        float xOffset = maxPositionOffset * magnitude * Mathf.PerlinNoise(seed + 3, seed + 3) * RandomMultiplier();
        float yOffset = maxPositionOffset * magnitude * Mathf.PerlinNoise(seed + 4, seed + 4) * RandomMultiplier();
        float zOffset = maxPositionOffset * magnitude * Mathf.PerlinNoise(seed + 5, seed + 5) * RandomMultiplier();
        return new Vector3(xOffset, yOffset, zOffset);
    }

    // Return a random rotation for the camera
    private Quaternion GetRotationalShake(float seed) {
        float yaw = maxYaw * magnitude * Mathf.PerlinNoise(seed, seed) * RandomMultiplier();
        float pitch = maxPitch * magnitude * Mathf.PerlinNoise(seed + 1, seed + 1) * RandomMultiplier();
        float roll = maxRoll * magnitude * Mathf.PerlinNoise(seed + 2, seed + 2) * RandomMultiplier();
        return Quaternion.Euler(yaw, pitch, roll);
    }

    private void ApplyCameraShake(Vector3 translation, Quaternion rotation) {
        _camera.localPosition = translation;
        _camera.localRotation = rotation;
    }

    private float RandomMultiplier() {
        return Random.Range(-1f, 1f);
    }

    // Sets the camera to its original position and rotation
    private void ResetCamera() {
        _camera.localRotation = Quaternion.Euler(Vector3.zero);
        _camera.localPosition = Vector3.zero;
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
