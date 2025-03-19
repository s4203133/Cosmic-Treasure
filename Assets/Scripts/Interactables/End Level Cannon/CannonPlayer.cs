using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    public class CannonPlayer : MonoBehaviour {
        private Transform thisTransform;
        [SerializeField] private float speed;
        [SerializeField] private VisualEffect smokeEffect;

        private void Start() {
            thisTransform = transform;
        }

        private void OnEnable() {
            if (smokeEffect != null) {
                smokeEffect.Play();
            }
        }

        void Update() {
            thisTransform.Translate(thisTransform.forward * speed * TimeValues.Delta, Space.World);
        }
    }
}