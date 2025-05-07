using UnityEngine;

namespace NR {

    public class ProjectileParent : MonoBehaviour {
        public static ProjectileParent Instance;

        [SerializeField]
        private GameObject projectilePrefab;

        private Projectile[] projectiles;
        private int projectileIndex;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        void Start() {
            projectiles = new Projectile[10];
            for (int i = 0; i < projectiles.Length; i++) {
                projectiles[i] = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();
                projectiles[i].gameObject.SetActive(false);
            }
        }

        public void SpawnProjectile(Transform launchTransform, float force) {
            Projectile projectileRB = projectiles[projectileIndex];
            projectileRB.Initialise(launchTransform.position, launchTransform.rotation, launchTransform.forward * force);

            projectileIndex++;
            if (projectileIndex >= projectiles.Length) {
                projectileIndex = 0;
            }
        }
    }
}
