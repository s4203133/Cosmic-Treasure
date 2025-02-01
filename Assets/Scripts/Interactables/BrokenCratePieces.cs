using UnityEngine;

namespace LMO.Interactables {

    public class BrokenCratePieces : MonoBehaviour {

        private Transform thisTransform;

        [SerializeField] private AnimationCurve sizeReduction;
        private float timer;
        private float duration;

        void Awake() {
            thisTransform = transform;
            duration = sizeReduction.keys[sizeReduction.keys.Length - 1].time;
        }

        void FixedUpdate() {
            // Reduce the size of the game object across the animation curve
            thisTransform.localScale = CalculateSize();
            timer += Time.deltaTime;

            // Destroy the game object once the time is up
            if (timer > duration) {
                Destroy(thisTransform.parent.gameObject);
            }
        }

        // Return the point of the animation curve based on the timer
        private Vector3 CalculateSize() {
            float size = sizeReduction.Evaluate(timer);
            return new Vector3(size, size, size);
        }
    }
}