using LMO;
using UnityEngine;

namespace NR {

    public class SlingshotJoint : GrapplePoint {
        private Rigidbody rb;
        private SlingshotDragger playerDrag;

        protected override void Start() {
            base.Start();
            rb = GetComponent<Rigidbody>();
            playerDrag = FindObjectOfType<SlingshotDragger>();
            interactActions.AddListener(playerDrag.DisconnectSlingshot);
        }

        public override void OnGrappled() {
            playerDrag.ConnectToSlingshot(rb);
        }

        public override void OnReleased() {
            playerDrag.DisconnectSlingshot();
        }
    }
}