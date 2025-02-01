using System.Collections;
using UnityEngine;

public class CameraControllerBasic : MonoBehaviour
{
    private Transform thisTransform;
    [SerializeField] private Transform target;
    private Vector3 offset;
    [Range(0f, 1f)]
    [SerializeField] private float smoothness;
    private float originalSmoothness;

    private void Awake() {
        thisTransform = transform;
        originalSmoothness = smoothness;
        offset = transform.position - target.position;
    }

    void FixedUpdate() {
        // Lerp to the target's position plus an offset
        Vector3 targetPosition = GetTartgetPosition();
        thisTransform.position = Vector3.Lerp(thisTransform.position, targetPosition, smoothness);
    }

    private Vector3 GetTartgetPosition() {
        return new Vector3(target.position.x + offset.x, thisTransform.position.y, target.position.z + offset.z);
    }

    // Temporarily increase the smoothness of the lerp to make the camera move towards it's target quicker
    public void QuickSnapToTarget() {
        smoothness = 0.5f;
        StartCoroutine(ReturnToNormalSmoothness());

        IEnumerator ReturnToNormalSmoothness() { 
            // Gradually transition the smoothness to its original value
            while(smoothness > originalSmoothness) {
                smoothness = Mathf.Lerp(smoothness, originalSmoothness, 0.1f);
                yield return null;
            }
            originalSmoothness = smoothness;
        }
    }
}
