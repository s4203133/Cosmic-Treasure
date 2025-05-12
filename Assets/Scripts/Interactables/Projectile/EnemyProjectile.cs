using UnityEngine;

namespace NR {
    public class EnemyProjectile : Projectile {
        private GameObject indicator;

        public void LoadIndicator(GameObject newIndicator) {
            indicator = newIndicator;
        }

        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);
            ProjectileParent.Instance.SpawnExplosion(transform.position);
            if (indicator != null) { 
                indicator.SetActive(false);
                indicator = null;
            }
            HideSelf();
        }
    }
}