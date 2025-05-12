using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace NR {

    [System.Serializable]
    public class ShipState {
        public string damage;
        public Mesh mesh;
    }

    public class ShipBoss : MonoBehaviour {
        [SerializeField]
        private ShipState[] damageStates;

        [SerializeField]
        private List<Material> damageMaterials;

        [SerializeField]
        private GameObject middleCannon;

        [SerializeField]
        private Transform shootPos;

        [SerializeField]
        private Transform shootTarget;

        public UnityEvent OnSink;
        public UnityEvent OnDeath;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private bool leftHit;
        private bool midHit;
        private bool rightHit;

        private bool isShooting = true;

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
                    ProjectileParent.Instance.SpawnIndicator(indicatePos, 0.425f, cannonShot as EnemyProjectile);
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