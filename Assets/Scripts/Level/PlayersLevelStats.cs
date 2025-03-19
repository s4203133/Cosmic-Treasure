using UnityEngine;

[CreateAssetMenu(menuName ="Player/Player Level Stats")]
public class PlayersLevelStats : ScriptableObject
{
    private int coinsCollected;
    public int CoinsCollected => coinsCollected;


    private int timesDied;
    public int TimesDied => timesDied;


    private float timeTook;
    public float TimeTook => timeTook;


    public void IncrementCoins() {
        coinsCollected++;
    }

    public void RegisterDeath() {
        timesDied++;
    }

    public void Tick(float time) {
        timeTook += time;
    }

    public void ResetStats() {
        coinsCollected = 0;
        timesDied = 0;
        timeTook = 0;
    }
}
