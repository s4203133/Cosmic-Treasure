using UnityEngine;

[CreateAssetMenu(menuName = "Plunder Score")]
public class PlunderScore : ScriptableObject {

    private float totalCoins;
    private float totalGems;
    private float totalRabbits;
    private float totalEnemies;

    public float coins;
    public float gems;
    public float rabbits;
    public float enemiesEliminated;

    public float coinPercentage;
    public float gemPercentage;
    public float rabbitPercentage;
    public float enemyPercentage;

    private float plunderScore;
    public float Score => plunderScore;

    public void SetTotals(int coins, int gems, int rabbits, int enemies) {
        totalCoins = coins;
        totalGems = gems;
        totalRabbits = rabbits;
        totalEnemies = enemies;
    }

    public void IncrementCoins() {
        coins++;
    }

    public void IncrementGems() {
        gems++;
    }

    public void IncrementRabbits() {
        rabbits++;
    }

    public void IncrementEnemies() {
        enemiesEliminated++;
    }

    public void CalculatePlunderScore() {
        coinPercentage = coins / totalCoins;
        gemPercentage = gems / totalGems;
        rabbitPercentage = rabbits / totalRabbits;
        enemyPercentage = enemiesEliminated / totalEnemies;

        plunderScore = ((coinPercentage + gemPercentage + rabbitPercentage + enemyPercentage) / 4) * 100;
    }
}
