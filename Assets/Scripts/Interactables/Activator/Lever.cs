using LMO;

namespace NR {
    public class Lever : Activator, ISpinnable {
        public void OnHit() {
            isActive = !isActive;
            OnActivate?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}


