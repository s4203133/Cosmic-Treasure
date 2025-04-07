using LMO;
using UnityEngine;

public class PalmTree : MonoBehaviour, IBreakable
{
    private InteractAnimation interaction;
    [SerializeField] private CoinSpawner coinSpawner;
    private bool coinsDropped;

    private void Start()
    {
        interaction = GetComponentInChildren<InteractAnimation>();
    }

    public void Break()
    {
        interaction.Play();

        if(!coinsDropped && coinSpawner != null) {
            coinsDropped = true;
            coinSpawner.SpawnCoin();
        }
    }
}
