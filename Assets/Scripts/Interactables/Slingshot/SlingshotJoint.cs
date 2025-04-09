using LMO;
using UnityEngine;
using System;
using System.Collections;

namespace NR {

    public class SlingshotJoint : GrapplePoint {
        private Transform slingshotTransform;
        public Transform SlingshotTransform => slingshotTransform;

        private Vector3 playerDragPos;
        public Vector3 PlayerDragPosition => playerDragPos;

        private Rigidbody rb;
        private SlingshotDragger playerDrag;
        public Action SlingshotUpdate;
        public Action SlingshotReleased;


        protected override void Start() {
            base.Start();
            rb = GetComponent<Rigidbody>();
            playerDrag = FindObjectOfType<SlingshotDragger>();
            interactActions.AddListener(playerDrag.DisconnectSlingshot);
        }

        public void SetParentTransform(Transform slingTransform) {
            slingshotTransform = slingTransform;
        }

        public override void OnGrappled() {
            StartCoroutine("UpdatePosition");
            playerDrag.ConnectToSlingshot(rb);
        }

        public override void OnReleased() {
            StopCoroutine("UpdatePosition");
            playerDrag.DisconnectSlingshot();
            SlingshotReleased?.Invoke();
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