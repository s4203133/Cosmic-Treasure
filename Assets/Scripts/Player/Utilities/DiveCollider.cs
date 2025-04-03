using UnityEngine;

namespace LMO {

    public class DiveCollider : MonoBehaviour {
        [SerializeField] private LayerMask hittableLayers;

        private void OnTriggerEnter(Collider other) {
            if (hittableLayers == (hittableLayers | (1 << other.gameObject.layer))) {
                PlayerDive.OnHitObject?.Invoke();
            }
        }
    }
}