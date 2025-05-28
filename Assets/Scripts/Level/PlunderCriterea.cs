using LMO;
using NR;
using UnityEngine;
using WWH;

public class PlunderCriterea : MonoBehaviour
{
    [SerializeField] private PlunderScore plunderScore;

    private void Start() {
        InitialisePlunderScore();
    }

    private void InitialisePlunderScore() {
        int numOfCoins = FindObjectsOfType(typeof(Coin)).Length;
        CoinSpawner[] coinSpawners = FindObjectsOfType(typeof(CoinSpawner)) as CoinSpawner[];
        for(int i = 0; i < coinSpawners.Length; i++) {
            SpawnCoinsCircular circularCoins = coinSpawners[i] as SpawnCoinsCircular;
            if(circularCoins != null ) {
                numOfCoins += circularCoins.amount;
                continue;
            }
            CoinSpawnDropped droppedCoins = coinSpawners[i] as CoinSpawnDropped;
            if(droppedCoins != null ) {
                numOfCoins += droppedCoins.amount;
            }
        }
        int numOfGems = FindObjectsOfType(typeof(Gem)).Length;
        int numOfRabbits = FindObjectsOfType(typeof(CageBreak)).Length;
        int numOfEnemies = AllEnemies();
        plunderScore.SetTotals(numOfCoins, numOfGems, numOfRabbits, numOfEnemies);

        plunderScore.coins = 0;
        plunderScore.rabbits = 0;
        plunderScore.enemiesEliminated = 0;
    }

    private void OnEnable() {
        Coin.OnCollectedCoin += plunderScore.IncrementCoins;
        Gem.OnGemCollected += plunderScore.IncrementGems;
        CageBreak.HitObject += plunderScore.IncrementRabbits;
        EnemyDeath.OnEnemyHit += plunderScore.IncrementEnemies;
        SeagullGrappleKill.OnEnemyHit += plunderScore.IncrementEnemies;
        EnemyCrushDie1.OnEnemyHit += plunderScore.IncrementEnemies;
    }

    private void OnDisable() {
        Coin.OnCollectedCoin -= plunderScore.IncrementCoins;
        Gem.OnGemCollected -= plunderScore.IncrementGems;
        CageBreak.HitObject -= plunderScore.IncrementRabbits;
        EnemyDeath.OnEnemyHit -= plunderScore.IncrementEnemies;
        SeagullGrappleKill.OnEnemyHit -= plunderScore.IncrementEnemies;
        EnemyCrushDie1.OnEnemyHit -= plunderScore.IncrementEnemies;
    }

    private int AllEnemies() {
        int returnValue;
        returnValue = FindObjectsOfType(typeof(Enemy1)).Length;
        returnValue += FindObjectsOfType(typeof(SegualEnemy)).Length;
        return returnValue;
    }
}
