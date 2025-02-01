using UnityEngine;

namespace LMO.Player {

    public abstract class GroundPoundComponent {

        public abstract void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded);

        public abstract void Disable();

    }
}