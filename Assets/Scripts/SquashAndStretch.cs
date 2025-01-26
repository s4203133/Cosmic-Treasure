using System;
using System.Collections;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField] private Transform transformToAffect;
    [SerializeField] private Axis axis;

    [SerializeField, Range(0f, 1f)] private float duration;

    [SerializeField] private float intensity;
    [SerializeField] private AnimationCurve motion;

    private Coroutine currentAnimation;

    bool playing;

    private bool affectX => (axis & Axis.X) != 0;
    private bool affectY => (axis & Axis.Y) != 0;
    private bool affectZ => (axis & Axis.Z) != 0;

    public void Play() {
        ValidateStart();
        currentAnimation = StartCoroutine(Animate());
    }

    private void ValidateStart() {
        if(axis == 0) {
            Debug.LogWarning("Axis is set to 'None'", this);
        }

        if(currentAnimation != null) {
            StopCoroutine(currentAnimation);
            transformToAffect.localScale = Vector3.one;
        }
    }

    private IEnumerator Animate() {
        float t = 0;
        while (t < duration) {
            Vector3 modifiedScale = Vector3.one;

            t += Time.deltaTime;
            float curvePos = t / duration;
            float value = motion.Evaluate(curvePos);
            float remappedValue = 1 + (value * (intensity - 1));

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