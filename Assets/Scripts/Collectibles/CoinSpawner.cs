using UnityEngine;

public abstract class CoinSpawner : MonoBehaviour
{
    [SerializeField] protected Coin coin;

    public abstract void SpawnCoin();
}
