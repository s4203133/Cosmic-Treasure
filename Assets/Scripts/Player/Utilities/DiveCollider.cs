using UnityEngine;

namespace LMO {

    public class DiveCollider : MonoBehaviour {
        [SerializeField] private PlayerDive playerDive;
        [SerializeField] private LayerMask hittableLayers;

        private void OnTriggerEnter(Collider other) {
            if (hittableLayers == (hittableLayers | (1 << other.gameObject.layer))) {
                playerDive.OnHitObject?.Invoke();
            }
        }
    }
}