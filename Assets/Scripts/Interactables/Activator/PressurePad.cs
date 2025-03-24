using LMO;

namespace NR {
    public class PressurePad : Activator, ICrushable {
        public void OnHit() {
            OnActivate?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}


