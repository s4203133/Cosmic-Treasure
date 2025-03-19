using UnityEngine;

namespace LMO {

    public class GroundPoundCollider : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            IBreakable breakable = other.GetComponent<IBreakable>();
            if (breakable != null) {
                breakable.Break();
            }
            else {
                ICrushable crushable = other.GetComponent<ICrushable>();
                if (crushable != null) {
                    crushable.OnHit();
                }
            }
        }
    }
}