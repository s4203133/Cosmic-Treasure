using UnityEngine;
using LMO;

namespace NR {
    /// <summary>
    /// What mesh (and associated materials) the object should have at different health increments.
    /// Should be ordered in reverse health order, with the last entry being minimum health and first the default.
    /// </summary>
    [System.Serializable]
    public class BreakMesh {
        public int health;
        public Mesh mesh;
        public Material[] materials;
    }

    /// <summary>
    /// What type of projectile damages a breakable object.
    /// Only checked when shot, so it can be changed at runtime if needed.
    /// </summary>
    public enum BreakableMode {
        NONE,
        PLAYER,
        ENEMY,
        ALL
    }

    /// <summary>
    /// Behaviour for objects that can be broken by projectiles.
    /// When shot (by appropriate projectile type) health is decremented and model updated if specified.
    /// When health reaches 0, the object is deactivated. This resets when the player dies.
    /// </summary>
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
