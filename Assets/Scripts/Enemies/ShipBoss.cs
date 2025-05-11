using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Unity.VisualScripting;

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

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private bool leftHit;
        private bool midHit;
        private bool rightHit;

        //Here for debugging
        public bool isShooting;
        public float shootSpeed;
        public bool shootHigh;
        private float shootTime;

        private bool firstHit = true;

        private void Awake() {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        //Debugging the shooting
        private void Update() {
            if (!isShooting) {
                return;
            }
            Vector3 launch = TrajectoryCalculator.CalculateLaunchVelocity(shootPos.position, shootTarget.position, shootSpeed, shootHigh);
            shootPos.forward = launch;
            shootTime += Time.deltaTime;
            if (shootTime > 2) {
                if (launch != Vector3.zero) {
                    shootTime = 0;
                    Projectile cannonShot = ProjectileParent.Instance.SpawnProjectile(shootPos, shootSpeed, true);
                    Vector3 indicatePos = shootTarget.position;
                    indicatePos.y += 0.5f;
                    ProjectileParent.Instance.SpawnIndicator(indicatePos, 0.425f, cannonShot as EnemyProjectile);
                }
            }
        }

        private void ShowDamage() {
            if (firstHit) {
                meshRenderer.SetMaterials(damageMaterials);
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
            meshFilter.mesh = newMesh;
            if (damageString == "lmr") {
                StartSinking();
            }
        }

        public void StartSinking() {
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