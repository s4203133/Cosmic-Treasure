using UnityEngine;

namespace LMO {

    public class SpawnCoinsCircular : CoinSpawner {

        [SerializeField] private int numberOfCoins;
        [SerializeField] private float radius;
        private Vector3 spawnPosition;
        [SerializeField] private float yOffset;

        private void Awake() {
            spawnPosition = transform.position;
        }

        public override void SpawnCoin() {
            SpawnCoinsInCircle();
        }

        private void SpawnCoinsInCircle() {
            // Get equally spaced position around a circle of a given radius to spawn the coins
            for (int i = 0; i < numberOfCoins; i++) {
                float segment = 2 * Mathf.PI * i / numberOfCoins;
                float horizontalValue = Mathf.Cos(segment);
                float verticalValue = Mathf.Sin(segment);
                Vector3 direction = new Vector3(horizontalValue, 0, verticalValue);
                Vector3 position = spawnPosition + direction * radius;
                position.y = spawnPosition.y + yOffset;
                CoinSpawnAnimated newCoin = (CoinSpawnAnimated)Instantiate(coin, spawnPosition, Quaternion.identity);
                newCoin.SetPosition(position);
                coin.name = "Coin " + i;
            }
        }
    }
}