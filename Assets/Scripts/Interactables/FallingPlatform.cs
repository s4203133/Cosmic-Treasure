using System.Collections;
using UnityEngine;

namespace NR {
    /// <summary>
    /// A platform that disappears shortlyafter being stepped on by the player.
    /// Triggered by the player's FallingPlatformDetector when landed on.
    /// </summary>
    public class FallingPlatform : MonoBehaviour {
        public float cooldown = 3;
        private Animator animator;
        private bool ready = true;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        public void Fall() {
            if (ready) {
                animator.SetBool("Fall", true);
            }
        }

        // Called by animator, once the diappear animation is visually finished.
        public void StartCooldown() {
            StartCoroutine(WaitForCooldown());
        }

        private IEnumerator WaitForCooldown() {
            yield return new WaitForSeconds(cooldown);
            animator.SetBool("Fall", false);
        }

        // Called by animator, once the reappear animation is visually finished.
        public void ResetPlatform() {
            ready = true;
        }
    }
}


