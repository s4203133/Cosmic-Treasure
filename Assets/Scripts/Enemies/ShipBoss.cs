using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private bool leftHit;
        private bool midHit;
        private bool rightHit;

        private bool firstHit = true;

        private void Awake() {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
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