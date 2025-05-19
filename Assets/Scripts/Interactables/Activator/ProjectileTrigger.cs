using UnityEngine;
using UnityEngine.Events;

namespace NR {
    /// <summary>
    /// An activator that triggers when shot with a projectile.
    /// Additionally calls a unity event for custom behaviour.
    /// </summary>
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