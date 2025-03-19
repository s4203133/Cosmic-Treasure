using System;
using System.Collections;
using UnityEngine;

namespace LMO {

    public class SquashAndStretch : MonoBehaviour {
        [SerializeField] private Transform transformToAffect;
        [SerializeField] private Axis axis;

        [SerializeField, Range(0f, 1f)] private float duration;

        [SerializeField] private float scale;
        [SerializeField] private AnimationCurve motion;

        private Coroutine currentAnimation;

        private bool affectX => (axis & Axis.X) != 0;
        private bool affectY => (axis & Axis.Y) != 0;
        private bool affectZ => (axis & Axis.Z) != 0;

        public void Play() {
            ValidateStart();
            currentAnimation = StartCoroutine(Animate());
        }

        // Check an axis has been assigned and stop the current animation if one is playing
        private void ValidateStart() {
            if (axis == 0) {
                Debug.LogWarning("Axis is set to 'None'", this);
            }

            if (currentAnimation != null) {
                StopCoroutine(currentAnimation);
                transformToAffect.localScale = Vector3.one;
            }
        }

        private IEnumerator Animate() {
            float t = 0;
            while (t < duration) {
                Vector3 modifiedScale = Vector3.one;

                t += TimeValues.Delta;
                float curvePos = t / duration;
                float value = motion.Evaluate(curvePos);
                float remappedValue = 1 + (value * (scale - 1));

                // Affect the chosen axis'
                // Any un-chosen axis' will be scaled in the opposite way  
                if (affectX) {
                    modifiedScale.x *= remappedValue;
                } else {
                    modifiedScale.x /= remappedValue;
                }

                if (affectY) {
                    modifiedScale.y *= remappedValue;
                } else {
                    modifiedScale.y /= remappedValue;
                }

                if (affectZ) {
                    modifiedScale.z *= remappedValue;
                } else {
                    modifiedScale.z /= remappedValue;
                }

                transformToAffect.localScale = modifiedScale;

                yield return null;
            }
            // Reset the scale after the animation has finished
            transformToAffect.localScale = Vector3.one;
        }
    }

    [Flags]
    public enum Axis {
        None = 0,
        X = 1,
        Y = 2,
        Z = 4,
    }
}