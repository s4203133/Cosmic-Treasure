using LMO;
using UnityEngine;

namespace NR {
    public class ButtonCrushable : Activator, ICrushable {
        public enum ButtonType { Reusable, SingleUse };

        [SerializeField]
        private ButtonType buttonType;

        private bool buttonReady = true;

        [SerializeField]
        private bool active = true;

        protected override void Awake() {
            base.Awake();
            switch (buttonType) {
                case ButtonType.Reusable:
                    animator.SetBool("Repeat", true);
                    break;
                case ButtonType.SingleUse:
                    animator.SetBool("Repeat", false);
                    break;
            }
        }

        public void OnHit() {
            if (active && buttonReady) {
                isActive = true;
                OnActivate?.Invoke();
                animator.SetBool("Hit", true);
                buttonReady = false;
            }
        }

        public void ActivateButton() {
            active = true;
        }

        //Called from the animation on reuseable buttons (never called on single-use).
        //This ensures the button isn't triggered repeatedly in quick succession
        public void ButtonReady() {
            animator.SetBool("Hit", false);
            buttonReady = true;
        }
    }
}


