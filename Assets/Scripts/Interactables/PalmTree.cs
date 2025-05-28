using LMO;
using System;
using UnityEngine;

public class PalmTree : MonoBehaviour, IBreakable
{
    private InteractAnimation interaction;
    [SerializeField] private CoinSpawner coinSpawner;
    private bool coinsDropped;

    public static Action HitObject;

    private void Start()
    {
        interaction = GetComponentInChildren<InteractAnimation>();
    }

    public void Break()
    {
        interaction.Play();
        HitObject?.Invoke();
        if (!coinsDropped && coinSpawner != null) {
            coinsDropped = true;
            coinSpawner.SpawnCoin();
        }
    }
}
