using UnityEngine;
using System;

namespace NR {
    public class EnemyProjectile : Projectile {

        private void OnCollisionEnter(Collision collision) {
            Debug.Log("Hit");
            ProjectileParent.Instance.SpawnExplosion(transform.position);
            HideSelf();
        }
    }
}