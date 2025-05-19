using LMO;
using UnityEngine;
using System;
using System.Collections;

namespace NR {
    /// <summary>
    /// Behaviour for the grappleable part of slingshots.
    /// While grappled, it notifies the associated Slingshot script through an Action, and notes the player's position.
    /// When released,it notifies with an Action to fire a projectile.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class SlingshotJoint : GrapplePoint {
        private Transform slingshotTransform;
        public Transform SlingshotTransform => slingshotTransform;

        private Vector3 playerDragPos;
        public Vector3 PlayerDragPosition => playerDragPos;

        [SerializeField, Tooltip("Points in the world for the 'draw string' line renderer to connect to.")]
        private Transform[] linePositions;
        private Vector3[] linePoints;

        private Rigidbody rb;
        private SlingshotDragger playerDrag;
        private LineRenderer lr;
        public Action SlingshotUpdate;
        public Action SlingshotReleased;



        protected override void Start() {
            base.Start();
            rb = GetComponent<Rigidbody>();
            lr = GetComponent<LineRenderer>();
            playerDrag = FindObjectOfType<SlingshotDragger>();
            interactActions.AddListener(playerDrag.DisconnectSlingshot);
            lr.positionCount = linePositions.Length;
            linePoints = new Vector3[linePositions.Length];
            
        }

        public void SetParentTransform(Transform slingTransform) {
            slingshotTransform = slingTransform;
        }

        public override void OnGrappled() {
            StartCoroutine("UpdatePosition");
            playerDrag.ConnectToSlingshot(rb);
            PlayerDeath.OnPlayerDied += OnReleased;
        }

        public override void OnReleased() {
            PlayerDeath.OnPlayerDied -= OnReleased;
            StopCoroutine("UpdatePosition");
            playerDrag.DisconnectSlingshot();
            SlingshotReleased?.Invoke();
        }

        private void LateUpdate() {
            if (linePositions.Length > 0) {
                for (int i = 0; i < linePoints.Length; i++) {
                    linePoints[i] = linePositions[i].position;
                }
                lr.SetPositions(linePoints);
            }
        }

        private IEnumerator UpdatePosition() {
            while (true) {
                playerDragPos = playerDrag.transform.position;
                SlingshotUpdate.Invoke();
                yield return null;
            }
        }
    }
}