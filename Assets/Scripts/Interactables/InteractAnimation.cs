using UnityEngine;

namespace LMO {

    public class InteractAnimation : MonoBehaviour {
        private Animator animator;

        void Start() {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other) {
            // When the player touches this object, play an animatation
            if (other.tag == "Player" || other.tag == "PlayerSpinAttack") {
                Play();
            }
        }

        public void Play() {
            animator.SetTrigger("Interact");
        }
    }
}
