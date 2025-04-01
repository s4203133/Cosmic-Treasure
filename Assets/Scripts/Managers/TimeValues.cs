using UnityEngine;

namespace LMO {

    public class TimeValues : MonoBehaviour {
        public static float Delta { get; private set; }
        public static float FixedDelta { get; private set; }

        void Start() {
            FixedDelta = Time.fixedDeltaTime;
        }

        void Update() {
            Delta = Time.deltaTime;
        }
    }
}