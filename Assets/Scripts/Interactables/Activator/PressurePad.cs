using LMO;

namespace NR {
    public class PressurePad : Activator, ICrushable {
        private bool buttonReady = true;

        public void OnHit() {
            if (buttonReady) {
                OnActivate?.Invoke();
                animator.SetBool("Hit", true);
                buttonReady = false;
            }
        }

        //Called from the animation on the button.
        //This ensures the button isn't triggered repeatedly in quick succession
        public void ButtonReady() {
            animator.SetBool("Hit", false);
            buttonReady = true;
        }
    }
}


