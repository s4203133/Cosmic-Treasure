using UnityEngine;

namespace NR {
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

