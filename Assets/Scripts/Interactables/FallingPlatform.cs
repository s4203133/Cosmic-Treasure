using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMO;

namespace NR {
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

        public void StartCooldown() {
            StartCoroutine(WaitForCooldown());
        }

        private IEnumerator WaitForCooldown() {
            yield return new WaitForSeconds(cooldown);
            animator.SetBool("Fall", false);
        }

        public void ResetPlatform() {
            ready = true;
        }
    }
}


