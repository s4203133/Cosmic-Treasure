using LMO;
using UnityEngine;

public class CoinSpawnDropped : CoinSpawner
{
    [SerializeField] private CoinDropped[] coins;

    private void Start()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].Initialse();
        }
    }

    public override void SpawnCoin()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].Drop();
        }
    }
}
