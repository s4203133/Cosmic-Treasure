using System.Collections;
using UnityEngine;

namespace LMO {

    public class CoinDropped : Coin {
        private Rigidbody rigidBody;
        private MeshRenderer mesh;
        private SphereCollider[] colliders;
        private Animator animator;

        private bool hasDropped;
        private bool hasBounced;

        private WaitForSeconds registerDropDelay;

        public void Initialse() {
            rigidBody = GetComponent<Rigidbody>();
            mesh = GetComponentInChildren<MeshRenderer>();
            colliders = GetComponentsInChildren<SphereCollider>();
            animator = GetComponent<Animator>();

            mesh.enabled = false;
            EnableCollision(false);
            rigidBody.isKinematic = true;

            hasDropped = false;
            hasBounced = false;
            registerDropDelay = new WaitForSeconds(0.5f);
        }

        public void Drop() {
            mesh.enabled = true;
            EnableCollision(true);
            rigidBody.isKinematic = false;
            StartCoroutine(SetDropped());
        }

        private IEnumerator SetDropped() {
            yield return registerDropDelay;
            hasDropped = true;
        }

        protected override void HitGround() {
            if (!hasDropped || hasBounced) {
                return;
            }
            rigidBody.isKinematic = true;
            animator.SetTrigger("Bounce");
            hasBounced = true;
        }

        private void EnableCollision(bool value) {
            for (int i = 0; i < colliders.Length; i++) {
                colliders[i].enabled = value;
            }
        }

        protected override IEnumerator ActivateCollision() {
            yield return null;
        }
    }
}