using UnityEngine;

namespace LMO {

    public abstract class CoinSpawner : MonoBehaviour {
        [SerializeField] protected Coin coin;

        public abstract void SpawnCoin();
    }
}