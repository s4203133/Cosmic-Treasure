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

        [HideInInspector] public float distanceFromPlayer;
        [HideInInspector] public float angleToPlayer;

        protected bool disconnectWhenPlayerFalls;

        [Space(15)]
        [SerializeField] private UnityEvent interactActions;

        public void Interact() {
            interactActions?.Invoke();
        }

        protected virtual void Start() {
            detectionRangeSqrd = detectionRange * detectionRange;
            disconnectWhenPlayerFalls = true;
        }

    #if UNITY_EDITOR

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

    #endif

    }
}