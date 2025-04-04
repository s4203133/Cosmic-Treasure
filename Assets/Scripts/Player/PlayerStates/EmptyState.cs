
using UnityEngine;

namespace LMO {

    public class EmptyState : PlayerBaseState {
        public EmptyState(PlayerController playerController) : base(playerController) {

        }

        public override void OnStateEnter() {
        }

        public override void OnStateExit() {
        }

        public override void OnStatePhysicsUpdate() {
        }

        public override void OnStateUpdate() {
        }

        public override void OnTriggerEnter(Collider collider) {
        }
    }
}