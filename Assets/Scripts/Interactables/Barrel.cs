using UnityEngine;

namespace LMO {

    public class Barrel : MonoBehaviour, IBreakable {

        private Rigidbody rigidBody;
        [SerializeField] private float hitForce;
        [SerializeField] private float upwardsForce;
        [SerializeField] private float torqueForce;

        private static Transform player;

        void Start() {
            rigidBody = GetComponent<Rigidbody>();
            if (player == null) {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public void Break() {
            rigidBody.isKinematic = false;
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.AddForce((transform.position - player.transform.position).normalized * hitForce, ForceMode.Impulse);
            rigidBody.AddTorque(GetRandomTorque() * hitForce, ForceMode.Impulse);
        }

        private Vector3 GetHitForce() {
            return ((transform.position - player.transform.position).normalized * hitForce) + Vector3.up * upwardsForce;
        }

        private Vector3 GetRandomTorque() {
            float RandomNumber() { return Random.Range(-torqueForce, torqueForce); }
            return new Vector3(RandomNumber(), RandomNumber(), RandomNumber());
        }
    }
}