using UnityEngine;

namespace NR {
    /// <summary>
    /// Adds functions to objects with animators that can be called by Unity Events.
    /// Streamlines the process of caling animations through events.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorEventHandler : MonoBehaviour {
        Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        public void SetAnimatorBoolTrue(string name) {
            animator.SetBool(name, true);
        }

        public void SetAnimatorBoolFalse(string name) {
            animator.SetBool(name, false);
        }
    }

}

