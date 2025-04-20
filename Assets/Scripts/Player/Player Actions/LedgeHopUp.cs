using System;
using UnityEngine;

namespace LMO {
    public class LedgeHopUp : MonoBehaviour {

        [SerializeField] private Transform thisTransform;
        private Rigidbody rigidBody;
        private PlayerStateMachine player;
        [SerializeField] private LayerMask detectableLayers;

        [SerializeField] private float force;
        [SerializeField] private float duration;
        private float timer;

        private bool isHoppingOntoLedge;

        public static Action OnLedgeHopUpStarted;
        public static Action OnLedgeHopUpEnded;

        private void Start() {
            Initialise();
        }

        private void Initialise() {
            rigidBody = thisTransform.GetComponent<Rigidbody>();
            player = thisTransform.GetComponent<PlayerStateMachine>();
        }

        private void Update() {
            if (isHoppingOntoLedge) {
                Countdown();
            }
        }

        private void StartLedgeClimbUp() {
            Debug.Log("**Climb Up Ledge");
            /* player.Deactivate();
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(Vector3.up + Vector3.forward * force, ForceMode.Impulse);
            timer = duration;
            isHoppingOntoLedge = true;
            OnLedgeHopUpStarted?.Invoke();
            */
        }

        private void Countdown() {
            timer -= TimeValues.Delta;
            if (timer < 0) {
                isHoppingOntoLedge = false;
                player.Activate();
                OnLedgeHopUpEnded?.Invoke();
            }
        }

        public void DetectLedge() {
/*            RaycastHit upperHit;
            RaycastHit lowerHit;
            Vector3 offset = Vector3.up * 0.25f;

            if(Physics.Raycast(thisTransform.position + offset, thisTransform.forward, out upperHit, 1, detectableLayers)) {
                return;
            }
            else if (Physics.Raycast(thisTransform.position - offset, thisTransform.forward, out lowerHit, 1, detectableLayers)) {
                StartLedgeClimbUp();
            }*/
        }
    }
}