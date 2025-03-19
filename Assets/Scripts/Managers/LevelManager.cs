using UnityEngine;

namespace LMO {

    public class LevelManager : MonoBehaviour {
        [SerializeField] private PlayersLevelStats stats;

        void Start() {
            stats.ResetStats();
        }

        private void OnEnable() {
            SubscribeEvents();
        }

        private void OnDisable() {
            UnsubscribeEvents();
        }

        void Update() {
            stats.Tick(TimeValues.Delta);
        }

        private void SubscribeEvents() {
            Coin.OnCoinCollected += stats.IncrementCoins;
        }

        private void UnsubscribeEvents() {
            Coin.OnCoinCollected -= stats.IncrementCoins;
        }
    }
}