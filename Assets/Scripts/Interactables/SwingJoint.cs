using UnityEngine;

namespace LMO {

    public class SwingJoint : MonoBehaviour {

        public float distanceFromPlayer;
        public float angleToPlayer;

        private MeshRenderer meshRenderer;

        private void Start() {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void Activate() {
            meshRenderer.material.color = Color.red;
        }

        public void Deactivate() {
            meshRenderer.material.color = Color.white;
        }
    }
}