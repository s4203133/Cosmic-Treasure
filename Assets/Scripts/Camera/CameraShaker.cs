using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
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

    private float duration;
    private float magnitude;
    private float intensity;

    private void OnEnable() {
        cameraShakes.SubscribeCameraShakeActions();
        SubscribeCameraShakes();
    }

    private void OnDisable() {
        cameraShakes.UnsubscribeCameraShakeActions();
        UnsubscribeCameraShakes();
    }

    private void Update() {
        ReduceShake();
        Shake();
    }

    private void ShakeCamera(CameraShake shake) {
        duration += shake.Duration;
        intensity += shake.Magnitude;
        duration = Mathf.Clamp(duration, 0.0f, 1.0f);
        intensity = Mathf.Clamp(intensity, 0.0f, 1.0f);
    }

    private void Shake() {
        magnitude = Mathf.Pow(intensity, 2);

        float seed = Time.time;
        Vector3 newPosition = GetTranslationalShake(seed);
        Quaternion newRotation = GetRotationalShake(seed);

        ApplyCameraShake(newPosition, newRotation);
    }

    private void ReduceShake() {
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

    private Vector3 GetTranslationalShake(float seed) {
        float xOffset = maxPositionOffset * magnitude * Mathf.PerlinNoise(seed + 3, seed + 3) * RandomMultiplier();
        float yOffset = maxPositionOffset * magnitude * Mathf.PerlinNoise(seed + 4, seed + 4) * RandomMultiplier();
        float zOffset = maxPositionOffset * magnitude * Mathf.PerlinNoise(seed + 5, seed + 5) * RandomMultiplier();
        return new Vector3(xOffset, yOffset, zOffset);
    }

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



[System.Serializable]
public class CameraShakeType {
    [SerializeField] protected CameraShake camShake;
    public delegate void CustomEvent(CameraShake shakey);
    public CustomEvent OnShake;

    public virtual void SubscribeToActions() {

    }

    public virtual void UnsubscribeToActions() {

    }

    protected void Shake() {
        OnShake?.Invoke(camShake);
    }
}

[System.Serializable]
public class SmallCameraShake : CameraShakeType {

    public override void SubscribeToActions() {
        PlayerGroundPoundState.OnLanded += Shake;
        IBreakable.OnBroken += Shake;
    }

    public override void UnsubscribeToActions() {
        PlayerGroundPoundState.OnLanded += Shake;
        IBreakable.OnBroken -= Shake;
    }
}

[System.Serializable]
public class MediumCameraShake : CameraShakeType {

    public override void SubscribeToActions() {
    }

    public override void UnsubscribeToActions() {
    }
}

[System.Serializable]
public class LargeCameraShake : CameraShakeType {

    public override void SubscribeToActions() {
    }

    public override void UnsubscribeToActions() {
    }
}

[System.Serializable]
public class CameraShakes {
    [SerializeField] private SmallCameraShake smallCameraShake;
    [SerializeField] private MediumCameraShake mediumCameraShake;
    [SerializeField] private LargeCameraShake largeCameraShake;

    public SmallCameraShake small => smallCameraShake;
    public MediumCameraShake medium => mediumCameraShake;
    public LargeCameraShake large => largeCameraShake;

    public void SubscribeCameraShakeActions() {
        smallCameraShake.SubscribeToActions();
        mediumCameraShake.SubscribeToActions();
        largeCameraShake.SubscribeToActions();
    }

    public void UnsubscribeCameraShakeActions() {
        smallCameraShake.UnsubscribeToActions();
        mediumCameraShake.UnsubscribeToActions();
        largeCameraShake.UnsubscribeToActions();
    }
}