using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    public class Coin : MonoBehaviour, IResettable {
        [SerializeField] private FloatVariable coinCounter;
        private Vector3 startPosition;

        [Header("COLLECT VFX")]
        [SerializeField] private VisualEffect collectedVFX;
        private float collectedVFXDuration;
        private WaitForSeconds disableVFXDelay;

        private Collider coinCollider;
        private WaitForSeconds activateCollisionDelay;

        public static Action OnCoinActivated;
        public static Action OnCoinCollected;

        private bool collected;

        private void Awake() {
            Initialise();
        }

        private void Start() {
            StartCoroutine(ActivateCollision());
        }

        protected virtual void Initialise() {
            startPosition = transform.position;
            coinCollider = GetComponent<Collider>();
            collectedVFXDuration = collectedVFX.GetVector2("LifetimeRange").y;
            disableVFXDelay = new WaitForSeconds(collectedVFXDuration);
            activateCollisionDelay = new WaitForSeconds(0.5f);
        }

        // If the player picks the coin up, play a VFX and destroy it after a delay, and destroy the coin
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                if (collected) {
                    return;
                }

                coinCounter.value++;
                OnCoinCollected?.Invoke();
                collectedVFX.transform.parent = null;
                collectedVFX.Play();
                StartCoroutine(DisableCollectVFX());
                gameObject.SetActive(false);
            }
            else if(other.gameObject.layer == 6)
            {
                HitGround();
            }
        }

        protected virtual IEnumerator ActivateCollision() {
            yield return activateCollisionDelay;
            OnCoinActivated?.Invoke();
            coinCollider.enabled = true;
        }

        private IEnumerator DisableCollectVFX() {
            yield return disableVFXDelay;
            Destroy(collectedVFX.gameObject, collectedVFXDuration);
            collectedVFX.gameObject.SetActive(false);
        }

        protected virtual void HitGround()
        {

        }

        public void Reset() {
            gameObject.SetActive(true);
            collected = false;
            collectedVFX.Reinit();
            transform.position = startPosition;
        }
    }
}