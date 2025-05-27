using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    public class Collectible : MonoBehaviour {
        [SerializeField] protected FloatVariable counter;
        private Vector3 startPosition;

        [Header("COLLECT VFX")]
        [SerializeField] private VisualEffect collectedVFX;
        private float collectedVFXDuration;
        private WaitForSeconds disableVFXDelay;

        [SerializeField] private LayerMask groundDetectionLayers;
        private Collider collectibleCollider;
        private WaitForSeconds activateCollisionDelay;

        public static Action OnActivated;
        public static Action OnCollected;

        public Action<Collectible> OnInstanceCollected; //NR

        private bool collected;

        private void Awake() {
            Initialise();
        }

        private void Start() {
            StartCoroutine(ActivateCollision());
        }

        protected virtual void Initialise() {
            startPosition = transform.position;
            collectibleCollider = GetComponent<Collider>();
            collectedVFXDuration = collectedVFX.GetVector2("LifetimeRange").y;
            disableVFXDelay = new WaitForSeconds(collectedVFXDuration);
            activateCollisionDelay = new WaitForSeconds(0.1f);
        }

        protected void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                if (collected) {
                    return;
                }
                // If the player picks the coin up, play a VFX and destroy it after a delay, and destroy the coin
                counter.value++;
                OnInstanceCollected?.Invoke(this);
                OnCollected?.Invoke();
                collectedVFX.transform.parent = null;
                collectedVFX.Play();
                StartCoroutine(DisableCollectVFX());
                gameObject.SetActive(false);
            }
            else if (groundDetectionLayers == (groundDetectionLayers | (1 << other.gameObject.layer))) {
                HitGround();
            }
        }

        protected virtual IEnumerator ActivateCollision() {
            yield return activateCollisionDelay;
            OnActivated?.Invoke();
            collectibleCollider.enabled = true;
        }

        private IEnumerator DisableCollectVFX() {
            yield return disableVFXDelay;
            Destroy(collectedVFX.gameObject, collectedVFXDuration);
            collectedVFX.gameObject.SetActive(false);
        }

        protected virtual void HitGround() {

        }

        //public void Reset() {
        //    gameObject.SetActive(true);
        //    collected = false;
        //    collectedVFX.Reinit();
        //    transform.position = startPosition;
        //}
    }
}