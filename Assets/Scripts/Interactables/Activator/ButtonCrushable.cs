using LMO;
using System;
using UnityEngine;

namespace NR {
    /// <summary>
    /// An activator that triggers when the player ground pounds onto it.
    /// Can be setup as single-use or reuseable.
    /// </summary>
    public class ButtonCrushable : Activator, ICrushable {
        public enum ButtonType { Reusable, SingleUse };

        [SerializeField]
        private ButtonType buttonType;

        [SerializeField]
        private MeshRenderer buttonRenderer;

        [SerializeField]
        private Color pressedColour;
        private Color startColour;

        private bool buttonReady = true;

        [SerializeField]
        private bool active = true;

        public static Action OnInteracted;

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
            startColour = buttonRenderer.material.color;
        }

        public void OnHit() {
            if (active && buttonReady) {
                isActive = true;
                OnActivate?.Invoke();
                OnInteracted?.Invoke();
                animator.SetBool("Hit", true);
                buttonReady = false;
                buttonRenderer.material.color = pressedColour;
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
            buttonRenderer.material.color = startColour;
        }
    }
}


