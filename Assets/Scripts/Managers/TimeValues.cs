using UnityEngine;

namespace LMO {

    public class TimeValues : MonoBehaviour {
        private static float deltaTime;
        public static float Delta => deltaTime;


        private static float fixedDeltaTime;
        public static float FixedDelta => fixedDeltaTime;

        void Start() {
            fixedDeltaTime = Time.fixedDeltaTime;
        }

        void Update() {
            deltaTime = Time.deltaTime;
        }
    }
}