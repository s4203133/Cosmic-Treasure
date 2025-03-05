using UnityEngine;

namespace LMO {

    public class CoinDisplay : MonoBehaviour {
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

            timer -= Time.deltaTime;
            if (timer <= 0) {
                CloseDisplay();
            }
        }

        private void SubscribeEvents() {
            Coin.OnCoinCollected += OpenDisplay;
        }

        private void UnsubscribeEvents() {
            Coin.OnCoinCollected -= OpenDisplay;
        }

        private void OpenDisplay() {
            if (closed) {
                animator.SetTrigger("Open");
                closed = false;
            }
            timer = fadeAfterDuration;
        }

        private void CloseDisplay() {
            animator.SetTrigger("Close");
            closed = true;
        }
    }
}