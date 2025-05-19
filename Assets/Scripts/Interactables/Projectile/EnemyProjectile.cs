using UnityEngine;

namespace NR {
    /// <summary>
    /// Variant of projectiles fired by enemies (ship boss).
    /// Spawns an explosion when it hits the ground, which kills the player.
    /// </summary>
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