using UnityEngine;

public class CameraControllerBasic : MonoBehaviour
{
    private Transform thisTransform;
    [SerializeField] private Transform target;
    private Vector3 offset;
    [Range(0f, 1f)]
    [SerializeField] private float smoothness;

    private void Awake() {
        thisTransform = transform;
        offset = transform.position - target.position;
    }

    void FixedUpdate() {
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, thisTransform.position.y, target.position.z + offset.z);
        thisTransform.position = Vector3.Lerp(thisTransform.position, targetPosition, smoothness);
    }
}
