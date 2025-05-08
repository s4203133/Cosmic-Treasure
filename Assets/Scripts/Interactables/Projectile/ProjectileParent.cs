using UnityEngine;
using System.Collections;

namespace NR {

    public class ProjectileParent : MonoBehaviour {
        public static ProjectileParent Instance;

        [SerializeField]
        private GameObject playerProjectile;

        [SerializeField] 
        private GameObject enemyProjectile;

        [SerializeField]
        private GameObject explosion;

        [SerializeField]
        private GameObject indicator;

        private Projectile[] playerProjectiles;
        private int playerIndex;
        private Projectile[] enemyProjectiles;
        private int enemyIndex;
        private ProjectileExplosion[] explosions;
        private int explosionIndex;
        private Animator[] indicators;
        private int indicatorIndex;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        void Start() {
            playerProjectiles = new Projectile[5];
            for (int i = 0; i < playerProjectiles.Length; i++) {
                playerProjectiles[i] = Instantiate(playerProjectile, transform).GetComponent<Projectile>();
                playerProjectiles[i].gameObject.SetActive(false);
            }
            enemyProjectiles = new Projectile[5];
            for (int i = 0; i < enemyProjectiles.Length; i++) {
                enemyProjectiles[i] = Instantiate(enemyProjectile, transform).GetComponent<Projectile>();
                enemyProjectiles[i].gameObject.SetActive(false);
            }
            explosions = new ProjectileExplosion[5];
            for (int i = 0; i < explosions.Length; i++) {
                explosions[i] = Instantiate(explosion, transform).GetComponent<ProjectileExplosion>();
                explosions[i].gameObject.SetActive(false);
            }
            indicators = new Animator[5];
            for (int i = 0; i < explosions.Length; i++) {
                indicators[i] = Instantiate(indicator, transform).GetComponent<Animator>();
                indicators[i].gameObject.SetActive(false);
            }
        }

        public void SpawnProjectile(Transform launchTransform, float force, bool enemy = false) {
            Projectile useProjectile;
            if (enemy) {
                useProjectile = enemyProjectiles[enemyIndex];
                enemyIndex++;
                if (enemyIndex >= enemyProjectiles.Length) {
                    enemyIndex = 0;
                }
            } else {
                useProjectile = playerProjectiles[playerIndex];
                playerIndex++;
                if (playerIndex >= playerProjectiles.Length) {
                    playerIndex = 0;
                }
            }
            useProjectile.Initialise(launchTransform.position, launchTransform.rotation, launchTransform.forward * force);
        }

        public void SpawnExplosion(Vector3 position) {
            explosions[explosionIndex].ShowExplosion(position);

            explosionIndex++;
            if (explosionIndex >= explosions.Length) { 
                explosionIndex = 0;
            }
        }

        public void SpawnIndicator(Vector3 position, float speed) { 
            var useIndicator = indicators[indicatorIndex];
            useIndicator.gameObject.SetActive(true);
            useIndicator.transform.position = position;
            
            useIndicator.SetFloat("Speed", speed);
            useIndicator.SetTrigger("Indicate");

            StartCoroutine(DisableIndicator(useIndicator.gameObject, 1 / speed));

            indicatorIndex++;
            if (indicatorIndex >= indicators.Length) { 
                indicatorIndex = 0; 
            }
        }

        private IEnumerator DisableIndicator(GameObject indicator, float seconds) {
            yield return new WaitForSeconds(seconds);
            indicator.SetActive(false);
        }
    }
}
