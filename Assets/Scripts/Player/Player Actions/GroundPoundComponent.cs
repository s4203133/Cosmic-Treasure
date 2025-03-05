using UnityEngine;

namespace LMO {

    public abstract class GroundPoundComponent {

        public abstract void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded);

        public abstract void Disable();

    }
}