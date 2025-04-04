using UnityEngine;

namespace LMO {

    public class SpinCollider : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            IBreakable breakable = other.GetComponent<IBreakable>();
            if (breakable != null) {
                breakable.Break();
            }
            else {
                ISpinnable spinnable = other.GetComponent<ISpinnable>();
                if(spinnable != null) {
                    spinnable.OnHit();
                }
            }
        }
    }
}