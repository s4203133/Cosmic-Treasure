using UnityEngine;
using UnityEngine.Events;

namespace NR {
    public class ProjectileTrigger : Activator, IShootable {
        public UnityEvent OnProjectileHit;

        [SerializeField]
        private bool destroyProjectile;

        public void OnShot(Projectile projectile) {
            if (destroyProjectile) {
                projectile.HideSelf();
            }
            OnProjectileHit.Invoke();
            isActive = true;
            OnActivate?.Invoke();
        }
    }
}