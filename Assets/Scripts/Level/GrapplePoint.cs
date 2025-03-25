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

        [Space(15)]
        [SerializeField] private UnityEvent interactActions;
        private MeshRenderer meshRenderer;

        public void Interact() {
            interactActions?.Invoke();
        }

        private void Start() {
            detectionRangeSqrd = detectionRange * detectionRange;
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void Activate() {
            meshRenderer.material.color = Color.red;
        }

        public void Deactivate() {
            meshRenderer.material.color = Color.white;
        }

    #if UNITY_EDITOR

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

    #endif

    }
}