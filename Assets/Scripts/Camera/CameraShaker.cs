using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Coroutine currentShake;

    [SerializeField] private CameraShakes cameraShakes;
 
    private void OnEnable() {
        SubscribeSmallCameraShakes();
    }

    private void OnDisable() {
        UnsubscribeSmallCameraShakes();
    }

    private void ShakeCamera(CameraShake shake) {
        if(currentShake != null) {
            StopCoroutine(currentShake);
        }
        currentShake = StartCoroutine(shake.ShakeCamera(_camera.transform));
    }

    // Small Camera Shakes

    private void SmallCameraShake() {
        ShakeCamera(cameraShakes.small);
    }

    private void SubscribeSmallCameraShakes() {
        PlayerGroundPoundState.OnLanded += SmallCameraShake;
        IBreakable.OnBroken += SmallCameraShake;
    }

    private void UnsubscribeSmallCameraShakes() {
        PlayerGroundPoundState.OnLanded += SmallCameraShake;
        IBreakable.OnBroken -= SmallCameraShake;
    }

    // Medium Camera Shakes

    private void MediumCameraShake() {
        ShakeCamera(cameraShakes.medium);
    }

    // Large Camera Shakes

    private void LargeCameraShake() {
        ShakeCamera(cameraShakes.Large);
    }
}

[System.Serializable]
public class CameraShakes {
    [SerializeField] private CameraShake smallCameraShake;
    [SerializeField] private CameraShake mediumCameraShake;
    [SerializeField] private CameraShake LargeCameraShake;

    public CameraShake small => smallCameraShake;
    public CameraShake medium => mediumCameraShake;
    public CameraShake Large => LargeCameraShake;
}
