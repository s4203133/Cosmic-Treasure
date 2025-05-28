using LMO;
using System;

namespace NR {
    /// <summary>
    /// Activator that can be triggered by the player's spin attack.
    /// </summary>
    public class Lever : Activator, ISpinnable {
        public static Action OnInteracted;
        public void OnHit() {
            isActive = !isActive;
            OnActivate?.Invoke();
            OnInteracted?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}


