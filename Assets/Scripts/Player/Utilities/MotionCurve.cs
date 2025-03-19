using UnityEngine;

namespace LMO {

    [CreateAssetMenu(menuName = "Player/Motion Curve", fileName = "New Motion Curve")]
    public class MotionCurve : ScriptableObject {

        [SerializeField] private AnimationCurve velocityCurve;
        private float timerLength;
        private float timer;

        public void Initialise() {
            timerLength = velocityCurve.keys[velocityCurve.length - 1].time;
        }

        public float CalculateValue(float maxValue) {
            if (timer < timerLength) {
                timer += TimeValues.Delta;
            } else {
                timer = timerLength;
            }
            return maxValue * velocityCurve.Evaluate(timer);
        }

        public void Reset() {
            timer = 0;
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