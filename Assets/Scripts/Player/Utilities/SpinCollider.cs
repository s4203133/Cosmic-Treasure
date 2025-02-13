using UnityEngine;
using LMO.Interfaces;

namespace LMO.Player {

    public class SpinCollider : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            IBreakable breakable = other.GetComponent<IBreakable>();
            if (breakable != null) {
                breakable.Break();
            }
        }
    }
}