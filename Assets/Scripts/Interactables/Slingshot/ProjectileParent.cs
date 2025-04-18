using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace NR {

    public class ProjectileParent : MonoBehaviour {
        public static ProjectileParent Instance;

        [SerializeField]
        private GameObject projectile;

        private Rigidbody[] projectiles;
        private int projectileIndex;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        void Start() {
            projectiles = new Rigidbody[5];
            for (int i = 0; i < projectiles.Length; i++) {
                projectiles[i] = Instantiate(projectile, transform).GetComponent<Rigidbody>();
                projectiles[i].gameObject.SetActive(false);
            }
        }

        public void SpawnProjectile(Transform launchTransform, float force) {
            Rigidbody projectileRB = projectiles[projectileIndex];
            projectileRB.gameObject.SetActive(true);
            projectileRB.transform.position = launchTransform.position;
            projectileRB.transform.rotation = launchTransform.rotation;
            projectileRB.velocity = launchTransform.forward * force;

            projectileIndex++;
            if (projectileIndex >= projectiles.Length) {
                projectileIndex = 0;
            }
        }
    }
}
