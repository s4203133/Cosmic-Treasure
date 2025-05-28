using System;
using System.Collections;
using UnityEngine;

namespace LMO {

    public class Cannon : MonoBehaviour {
        [SerializeField] private Animator promptUIAnimator;
        [SerializeField] private JumpIntoCannon jumpInCannon;

        private BoxCollider boxCollision;

        private bool playerInRange;

        [Header("Launching Cannon")]
        [SerializeField] private LaunchCannon launchCannon;
        private WaitForSeconds launchDelay;
        [SerializeField] private float launchDelayTime;

        public static Action OnJumpingInCannon;
        public static Action OnEnteredCannon;
        public static Action OnCannonLaunched;

        private bool hasBeenInteractedWith;

        [Header("SFX")]
        [SerializeField] private AudioSource enteredCannonSound;
        [SerializeField] private AudioSource fireCannonSound;

        private void Start() {
            boxCollision = GetComponent<BoxCollider>();
            launchDelay = new WaitForSeconds(launchDelayTime);
        }

        private void OnEnable() {
            OnEnteredCannon += LaunchCannon;
            //InputHandler.jumpPerformed += JumpIntoCannon;
        }

        private void OnDisable() {
            OnEnteredCannon -= LaunchCannon;
            InputHandler.jumpPerformed -= JumpIntoCannon;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                InputHandler.jumpPerformed += JumpIntoCannon;

                playerInRange = true;
                promptUIAnimator.SetTrigger("FadeIn");
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.tag == "Player") {
                InputHandler.jumpPerformed -= JumpIntoCannon;

                playerInRange = false;
                promptUIAnimator.SetTrigger("FadeOut");
            }
        }

        private void JumpIntoCannon() {
            if (!playerInRange || hasBeenInteractedWith) {
                return;
            }

            OnJumpingInCannon?.Invoke();
            jumpInCannon.Play();
            boxCollision.enabled = false;
            promptUIAnimator.SetTrigger("FadeOut");
            hasBeenInteractedWith = true;
        }

        private void LaunchCannon() {
            enteredCannonSound.Play();
            StartCoroutine(LaunchPlayerOutCannon());
        }

        private IEnumerator LaunchPlayerOutCannon() {
            yield return launchDelay;
            OnCannonLaunched?.Invoke();
            launchCannon.Launch();
            fireCannonSound.Play();
        }
    }
}