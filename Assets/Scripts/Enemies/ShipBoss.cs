using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using LMO;

namespace NR {
    /// <summary>
    /// A serialized class used in ShipBoss's 'damageStates' property.
    /// Associates the damage to the ship (represented as a string) with the corresponding mesh.
    /// </summary>
    [System.Serializable]
    public class ShipState {
        public string damage;
        public Mesh mesh;
    }

    /// <summary>
    /// Behaviour for the boss at the end of level 1.
    /// Periodically fires cannonballs aimed at the player once activated.
    /// When a target in the prefab is hit, the damage is shown in the mesh.
    /// After all three targets are hit, the ship sinks and an event is called.
    /// </summary>
    public class ShipBoss : MonoBehaviour, IResettable {
        [SerializeField]
        private ShipState[] damageStates;

        // The damaged meshes have more materials than the undamaged mesh, so they are swapped at runtime.
        [SerializeField]
        private List<Material> damageMaterials;

        private List<Material> startMaterials = new List<Material>();

        [SerializeField]
        private GameObject middleCannon;

        [SerializeField]
        private Transform shootPos;

        [SerializeField]
        private Transform shootTarget;

        [SerializeField]
        private GameObject[] targets;

        public UnityEvent OnSink;
        public UnityEvent OnDeath;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private bool leftHit;
        private bool midHit;
        private bool rightHit;

        private bool isShooting = false;

        [SerializeField]
        private float shootSpeed = 100;

        [SerializeField]
        private float shootDelay = 2;

        [SerializeField]
        private bool shootHigh = true;

        private float shootTime;

        private bool firstHit = true;

        private void Awake() {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.GetMaterials(startMaterials);
        }

        public void Activate() {
            isShooting = true;
            StartCoroutine(ShootingLoop());
        }

        private IEnumerator ShootingLoop() {
            while (isShooting) {
                shootTime += Time.deltaTime;
                if (shootTime > shootDelay) {
                    Vector3 launch = TrajectoryCalculator.CalculateLaunchVelocity(shootPos.position, shootTarget.position, shootSpeed, shootHigh);
                    if (launch != Vector3.zero) {
                        shootPos.forward = launch;
                        shootTime = 0;
                        Projectile cannonShot = ProjectileParent.Instance.SpawnProjectile(shootPos, shootSpeed, true);
                        Vector3 indicatePos = shootTarget.position;
                        indicatePos.y += 0.5f;
                        ProjectileParent.Instance.SpawnIndicator(indicatePos, 0.5f, cannonShot as EnemyProjectile);
                    }
                }
                yield return null;
            }
        }

        /// <summary>
        /// When a target is hit, change to the appropriate mesh.
        /// The damage is represented by a string.
        /// If all are hit, start sinking.
        /// </summary>
        private void ShowDamage() {
            if (firstHit) {
                _meshRenderer.SetMaterials(damageMaterials);
                firstHit = false;
            }
            string damageString = "";
            damageString += leftHit ? "l" : "";
            if (midHit) {
                damageString += "m";
                middleCannon.SetActive(false);
            }
            damageString += rightHit ? "r" : "";
            Mesh newMesh = (from state in damageStates
                          where state.damage == damageString
                          select state.mesh).ToList()[0];
            _meshFilter.mesh = newMesh;
            if (damageString == "lmr") {
                StartSinking();
            }
        }

        public void StartSinking() {
            isShooting = false;
            OnSink.Invoke();
        }

        //Called by animator
        public void DoneSinking() {
            OnDeath.Invoke();
        }

        // As this inherits from IResettable, it calls this when the player dies.
        public void Reset() {
            leftHit = false;
            midHit = false;
            rightHit = false;
            firstHit = true;
            _meshRenderer.SetMaterials(startMaterials);
            _meshFilter.mesh = damageStates[0].mesh;
            middleCannon.SetActive(true);
            foreach (var target in targets) {
                target.SetActive(true);
            }
            isShooting = false;
        }

        // Below are called by targets, using unity events.

        public void HitLeft() {
            leftHit = true;
            ShowDamage();
        }

        public void HitMid() {
            midHit = true;
            ShowDamage();
        }

        public void HitRight() {
            rightHit = true;
            ShowDamage();
        }
    }
}