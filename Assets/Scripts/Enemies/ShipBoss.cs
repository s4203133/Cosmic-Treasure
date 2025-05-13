using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using LMO;

namespace NR {

    [System.Serializable]
    public class ShipState {
        public string damage;
        public Mesh mesh;
    }

    public class ShipBoss : MonoBehaviour, IResettable {
        [SerializeField]
        private ShipState[] damageStates;

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
                Vector3 launch = TrajectoryCalculator.CalculateLaunchVelocity(shootPos.position, shootTarget.position, shootSpeed, shootHigh);
                shootTime += Time.deltaTime;
                if (shootTime > shootDelay && launch != Vector3.zero) {
                    shootPos.forward = launch;
                    shootTime = 0;
                    Projectile cannonShot = ProjectileParent.Instance.SpawnProjectile(shootPos, shootSpeed, true);
                    Vector3 indicatePos = shootTarget.position;
                    indicatePos.y += 0.5f;
                    ProjectileParent.Instance.SpawnIndicator(indicatePos, 0.5f, cannonShot as EnemyProjectile);
                }
                yield return null;
            }
        }

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