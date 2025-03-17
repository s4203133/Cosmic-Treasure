using LMO;
using UnityEngine;

public class PalmTree : MonoBehaviour, IBreakable
{
    private Animator animator;
    [SerializeField] private CoinSpawner coinSpawner;
    private bool coinsDropped;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Break()
    {
        animator.SetTrigger("Interact");

        if(!coinsDropped && coinSpawner != null) {
            coinsDropped = true;
            coinSpawner.SpawnCoin();
        }
    }
}
