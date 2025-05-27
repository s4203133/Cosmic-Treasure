using System;
using UnityEngine;
using UnityEngine.Events;

namespace LMO {

    public class GrapplePoint : MonoBehaviour {

        [SerializeField] private float detectionRange;
        private float detectionRangeSqrd;
        public float DetectionRange => detectionRange;
        public float DetectionRangeSqrd => detectionRangeSqrd;

        [SerializeField] private bool playerMustFacePointToConnect;
        public bool PlayerMustFacePointToConnect => playerMustFacePointToConnect;

        protected bool canConnect;
        public bool CanConnect => canConnect;

        [HideInInspector] public float distanceFromPlayer;
        [HideInInspector] public float angleToPlayer;

        protected bool disconnectWhenPlayerFalls;

        [Space(15)]
        [SerializeField] protected UnityEvent interactActions;
        public static Action<GameObject> GrapplePointNoLongerOnScreen;
        public static Action<GameObject> GrapplePointBackOnScreen;

        public void Interact() {
            interactActions?.Invoke();
        }


        //<NR>
        public virtual void OnGrappled() { }
        public virtual void OnReleased() { }
        //</NR>

        private void OnBecameInvisible() {
            GrapplePointNoLongerOnScreen?.Invoke(gameObject);
        }

        private void OnBecameVisible() {
            GrapplePointBackOnScreen?.Invoke(gameObject);
        }

        protected virtual void Start() {
            detectionRangeSqrd = detectionRange * detectionRange;
            disconnectWhenPlayerFalls = true;
            canConnect = true;
        }


    #if UNITY_EDITOR

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

    #endif

    }
}