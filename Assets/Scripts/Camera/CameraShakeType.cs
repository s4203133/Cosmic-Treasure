using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class CameraShakeType {
        [SerializeField] protected CameraShake camShake;
        public delegate void CustomEvent(CameraShake shake);
        public CustomEvent OnShake;

        public void Shake() {
            OnShake?.Invoke(camShake);
        }
    }
}