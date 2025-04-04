using UnityEngine;

namespace LMO {
    public class UIDisplay : MonoBehaviour {
        private Animator animator;
        [SerializeField] private float fadeAfterDuration;
        private float timer;
        bool closed;

        private void OnEnable() => SubscribeEvents();

        private void OnDisable() => UnsubscribeEvents();

        private void Start() {
            animator = GetComponent<Animator>();
        }

        private void Update() {
            if (closed) return;

            timer -= TimeValues.Delta;
            if (timer <= 0) {
                CloseDisplay();
            }
        }

        protected virtual void SubscribeEvents() {

        }

        protected virtual void UnsubscribeEvents() {

        }

        protected void OpenDisplay() {
            if (closed) {
                animator.SetTrigger("Open");
                closed = false;
            }
            timer = fadeAfterDuration;
        }

        protected void CloseDisplay() {
            animator.SetTrigger("Close");
            closed = true;
        }
    }
}