using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    public class Coin : MonoBehaviour {
        [SerializeField] private FloatVariable coinCounter;

        [Header("COLLECT VFX")]
        [SerializeField] private VisualEffect collectedVFX;
        private float collectedVFXDuration;

        private Collider coinCollider;

        public static Action OnCoinActivated;
        public static Action OnCoinCollected;

        private void Awake() {
            Initialise();
        }

        private void Start() {
            StartCoroutine(ActivateCollision());
        }

        protected virtual void Initialise() {
            coinCollider = GetComponent<Collider>();
            collectedVFXDuration = collectedVFX.GetVector2("LifetimeRange").y;
        }

        // If the player picks the coin up, play a VFX and destroy it after a delay, and destroy the coin
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                coinCounter.value++;
                OnCoinCollected?.Invoke();
                collectedVFX.transform.parent = null;
                collectedVFX.Play();
                Destroy(collectedVFX.gameObject, collectedVFXDuration);
                Destroy(gameObject);
            }
            else if(other.gameObject.layer == 6)
            {
                HitGround();
            }
        }

        protected virtual IEnumerator ActivateCollision() {
            yield return new WaitForSeconds(0.5f);
            OnCoinActivated?.Invoke();
            coinCollider.enabled = true;
        }

        protected virtual void HitGround()
        {

        }
    }
}