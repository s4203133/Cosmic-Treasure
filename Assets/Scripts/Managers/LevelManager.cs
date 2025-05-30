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
            Coin.OnCollected += stats.IncrementCoins;
            PlayerDeath.OnPlayerDied += stats.RegisterDeath;
        }

        private void UnsubscribeEvents() {
            Coin.OnCollected -= stats.IncrementCoins;
            PlayerDeath.OnPlayerDied -= stats.RegisterDeath;
        }
    }
}