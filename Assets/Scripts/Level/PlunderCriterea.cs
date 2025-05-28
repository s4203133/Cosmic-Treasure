using LMO;
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
        int numOfGems = FindObjectsOfType(typeof(Gem)).Length;
        int numOfRabbits = FindObjectsOfType(typeof(RabbitCounter)).Length;
        int numOfEnemies = AllEnemies();
        plunderScore.SetTotals(numOfCoins, numOfGems, numOfRabbits, numOfEnemies);
    }

    private void OnEnable() {
        Coin.OnCollectedCoin += plunderScore.IncrementCoins;
        Gem.OnGemCollected += plunderScore.IncrementGems;
        RabbitCounter.OnCollectedRabbit += plunderScore.IncrementRabbits;
        EnemyDeath.OnEnemyHit += plunderScore.IncrementEnemies;
        SeagullGrappleKill.OnEnemyHit += plunderScore.IncrementEnemies;
    }

    private int AllEnemies() {
        int returnValue;
        returnValue = FindObjectsOfType(typeof(Enemy1)).Length;
        returnValue += FindObjectsOfType(typeof(SegualEnemy)).Length;
        return returnValue;
    }
}
