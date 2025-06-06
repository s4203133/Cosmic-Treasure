using UnityEngine;

namespace NR {
    /// <summary>
    /// Manager script that spawns projectiles without needing to instantiate/destroy them every time (for performance).
    /// Spawns projectiles, explosions & indicators when the game starts (5 of each type), which are disabled.
    /// Whenevera projectile (or associated object) is needed, one of these 5 are activated and used.
    /// </summary>
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

        /// <summary>
        /// Spawns a projectile (either player or enemy) and launches it as needed.
        /// </summary>
        /// <param name="launchTransform">A transform that is angled in the direction to fire the shot.</param>
        /// <param name="force">The power of the shot.</param>
        /// <param name="enemy">Whether to spawn an enemy projectile (true) or player projectile (false).</param>
        /// <returns>A reference to the used projectile's script.</returns>
        public Projectile SpawnProjectile(Transform launchTransform, float force, bool enemy = false) {
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
            return useProjectile;
        }

        public void SpawnExplosion(Vector3 position) {
            explosions[explosionIndex].ShowExplosion(position);

            explosionIndex++;
            if (explosionIndex >= explosions.Length) { 
                explosionIndex = 0;
            }
        }

        public void SpawnIndicator(Vector3 position, float speed, EnemyProjectile projectile) { 
            var useIndicator = indicators[indicatorIndex];
            useIndicator.gameObject.SetActive(true);
            useIndicator.transform.position = position;
            
            useIndicator.SetFloat("Speed", speed);
            useIndicator.SetTrigger("Indicate");

            projectile.LoadIndicator(useIndicator.gameObject);

            indicatorIndex++;
            if (indicatorIndex >= indicators.Length) { 
                indicatorIndex = 0; 
            }
        }
    }
}
