using LMO;

namespace NR {
    public class EnemyTrigger : Activator, ISpinnable {
        public void OnHit() {
            isActive = !isActive;
            OnActivate?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}


