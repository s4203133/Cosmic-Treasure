using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Shake", fileName = "New Camera Shake")]
public class CameraShake : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    public IEnumerator ShakeCamera(Transform camTransform) {
        Vector3 originalPosition = camTransform.localPosition;
        float t = 0;
        while(t < duration) {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);

            Vector3 newPosition = originalPosition + new Vector3(x, 0, y);
            camTransform.localPosition = newPosition;

            t += Time.deltaTime;

            yield return null;
        }
    }
}

[System.Serializable]
public class CameraShakeComponent {
    [SerializeField] private CameraShake cameraShake;
}

