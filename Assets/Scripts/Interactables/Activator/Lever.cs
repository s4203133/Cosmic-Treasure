using LMO;

namespace NR {
    public class Lever : Activator, ISpinnable {
        public void OnHit() {
            OnActivate?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}


