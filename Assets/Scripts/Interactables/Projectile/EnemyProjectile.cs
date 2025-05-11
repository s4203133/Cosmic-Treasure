using UnityEngine;

namespace NR {
    public class EnemyProjectile : Projectile {
        private GameObject indicator;

        public void LoadIndicator(GameObject newIndicator) {
            indicator = newIndicator;
        }

        private void OnCollisionEnter(Collision collision) {
            ProjectileParent.Instance.SpawnExplosion(transform.position);
            if (indicator != null) { 
                indicator.SetActive(false);
                indicator = null;
            }
            HideSelf();
        }
    }
}