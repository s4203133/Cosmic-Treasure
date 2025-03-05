using UnityEngine;

namespace LMO {

    [CreateAssetMenu(menuName = "Camera/Camera Shake", fileName = "New Camera Shake")]
    public class CameraShake : ScriptableObject {
        [SerializeField] private float duration;
        [SerializeField] private float magnitude;

        public float Duration => duration;
        public float Magnitude => magnitude;
    }
}