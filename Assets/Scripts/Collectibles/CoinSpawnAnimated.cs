using UnityEngine;

namespace LMO {

    public class CoinSpawnAnimated : Coin {
        [SerializeField] private float spawnAnimateSmoothness;
        private Transform thisTransform;
        private Vector3 position;

        private CoinCollector collector;

        protected override void Initialise() {
            base.Initialise();
            thisTransform = transform;
            collector = GetComponentInChildren<CoinCollector>();
        }

        private void FixedUpdate() {
            AnimateCoin();
        }

        private void AnimateCoin() {
            if (collector.CoinInRange) {
                return;
            }
            thisTransform.position = Vector3.Lerp(thisTransform.position, position, spawnAnimateSmoothness);
        }

        public void SetPosition(Vector3 newPosition) {
            position = newPosition;
        }
    }
}