using System;
using UnityEngine;

namespace LMO {

    public class Barrel : MonoBehaviour, IBreakable {

        private Transform thisTransform;
        private Rigidbody rigidBody;
        [SerializeField] private float hitForce;
        [SerializeField] private float upwardsForce;
        [SerializeField] private float torqueForce;

        private static Transform player;

        public static Action HitObject;

        void Start() {
            thisTransform = transform;
            rigidBody = GetComponent<Rigidbody>();
            if (player == null) {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public void Break() {
            HitObject?.Invoke();
            rigidBody.isKinematic = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.AddForce((thisTransform.position - player.transform.position).normalized * hitForce, ForceMode.Impulse);
            rigidBody.AddTorque(GetRandomTorque() * hitForce, ForceMode.Impulse);
        }

        private Vector3 GetHitForce() {
            return ((thisTransform.position - player.transform.position).normalized * hitForce) + (thisTransform.up * upwardsForce);
        }

        private Vector3 GetRandomTorque() {
            float RandomNumber() { return UnityEngine.Random.Range(-torqueForce, torqueForce); }
            return new Vector3(RandomNumber(), RandomNumber(), RandomNumber());
        }
    }
}