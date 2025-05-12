using UnityEngine;
using LMO;

namespace NR {
    [System.Serializable]
    public class BreakMesh {
        public int health;
        public Mesh mesh;
        public Material[] materials;
    }

    public enum BreakableMode {
        NONE,
        PLAYER,
        ENEMY,
        ALL
    }

    public class Breakable : MonoBehaviour, IShootable, IResettable {
        public BreakableMode mode;

        [SerializeField]
        private int hitsToBreak = 1;
        private int currentHealth;

        [SerializeField, Tooltip("Mesh/Materials at different health values. Organise in reverse order (Max health first).")]
        private BreakMesh[] modelProgression;

        private int meshIndex;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private void Awake() {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            Reset();
        }

        public void OnShot(Projectile projectile) {
            bool takeDamage = true;
            switch (mode) {
                case BreakableMode.NONE:
                    return;
                case BreakableMode.PLAYER:
                    if (projectile as EnemyProjectile != null) {
                        takeDamage = false;
                    }
                    break;
                case BreakableMode.ENEMY:
                    if (projectile as EnemyProjectile == null) {
                        takeDamage = false;
                    }
                    break;
            }

            if (takeDamage) {
                currentHealth--;
                if (currentHealth <= 0) { 
                    gameObject.SetActive(false);
                    return;
                }
                if (meshIndex < modelProgression.Length - 1) { 
                    if (modelProgression[meshIndex + 1].health >= currentHealth) {
                        UpdateModel(meshIndex + 1);
                    }
                }
            }
        }

        public void Reset() {
            gameObject.SetActive(true);
            currentHealth = hitsToBreak;
            UpdateModel(0);
        }

        private void UpdateModel(int index) {
            meshIndex = index;
            _meshFilter.mesh = modelProgression[index].mesh;
            _meshRenderer.materials = modelProgression[index].materials;
        }
    }
}
