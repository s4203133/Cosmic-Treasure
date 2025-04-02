using UnityEngine;

namespace LMO {

    public class ParrotAnimationController : MonoBehaviour {


        private Animator animator;

        void Start () { 
            animator = GetComponent<Animator>();
        }

        private void OnEnable() {
            PlayerHover.OnHoverStarted += PlayHoverAnimation;
            PlayerHover.OnHoverContinued += PlayHoverAnimation;
            PlayerHover.OnHoverEnded += StopHoverAnimation;
        }

        private void OnDisable() {
            PlayerHover.OnHoverStarted -= PlayHoverAnimation;
            PlayerHover.OnHoverContinued -= PlayHoverAnimation;
            PlayerHover.OnHoverEnded -= StopHoverAnimation;
        }

        private void PlayHoverAnimation() {
            animator.SetBool("Hovering", true);
        }

        private void StopHoverAnimation() {
            animator.SetBool("Hovering", false);
        }
    }
}