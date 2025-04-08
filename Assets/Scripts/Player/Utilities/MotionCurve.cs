using UnityEngine;

namespace LMO {

    [CreateAssetMenu(menuName = "Player/Motion Curve", fileName = "New Motion Curve")]
    public class MotionCurve : ScriptableObject {

        [SerializeField] private AnimationCurve velocityCurve;
        private float timerLength;
        private float timer;

        public void Initialise() {
            timerLength = velocityCurve.keys[velocityCurve.length - 1].time;
            PlayerMovement.OnMoveStopped += ResetTimer;
        }

        public void OnDisable() {
            PlayerMovement.OnMoveStopped -= ResetTimer;
        }

        public float CalculateValue(float maxValue) {
            timer += TimeValues.FixedDelta;
            return maxValue * velocityCurve.Evaluate(timer);
        }

        public void ResetTimer() {
            timer = 0f;
        }

        public bool Finished() {
            return (timer >= timerLength);
        }

        public void SetTimer(float value) {
            timer = value;
        }

        public float GetTimerValue() {
            return timer;
        }
    }
}