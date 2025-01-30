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

    public void SetTarget(GameObject newTarget) {
        target = newTarget.transform;
        offset = transform.position - target.position;
    }

    void FixedUpdate() {
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, thisTransform.position.y, target.position.z + offset.z);
        thisTransform.position = Vector3.Lerp(thisTransform.position, targetPosition, smoothness);
    }

    public void QuickSnapToTarget() {
        smoothness = 0.5f;
        StartCoroutine(ReturnToNormalSmoothness());

        IEnumerator ReturnToNormalSmoothness(){ 
            while(smoothness > originalSmoothness) {
                smoothness = Mathf.Lerp(smoothness, originalSmoothness, 0.1f);
                yield return null;
            }
            originalSmoothness = smoothness;
        }
    }
}
