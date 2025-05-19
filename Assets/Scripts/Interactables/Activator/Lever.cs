using LMO;

namespace NR {
    /// <summary>
    /// Activator that can be triggered by the player's spin attack.
    /// </summary>
    public class Lever : Activator, ISpinnable {
        public void OnHit() {
            isActive = !isActive;
            OnActivate?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}


