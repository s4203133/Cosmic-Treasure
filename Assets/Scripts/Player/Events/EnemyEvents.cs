using LMO;
using NR;
using UnityEngine;

public class EnemyEvents : MonoBehaviour, ICustomEvent {
    private PlayerAudioManager audioManager;

    public void Initialise(EventManager manager) {
        PlayerEventManager player = manager as PlayerEventManager;
        audioManager = player.Controller.playerAudioManager;
    }

    public void SubscribeEvents() {
        EnemyDeath.OnEnemyHit += audioManager.PlayKillCrab;
        EnemyCrushDie.OnEnemyHit += audioManager.PlayKillCrab;
        EnemyCrushDie1.OnEnemyHit += audioManager.PlayKillCrab;
        SeagullGrappleKill.OnEnemyHit += audioManager.PlaySlamSeagull;
        PlayerHealth.OnDamageTaken += audioManager.PlayHitByEnemy;
        PlayerHealth.PlayerKilledByEnemy += audioManager.PlayDeath;
        LevelDeathCatcher.OnPlayerFellOutLevel += audioManager.PlayDeath;
    }

    public void UnsubscribeEvents() {
        EnemyDeath.OnEnemyHit -= audioManager.PlayKillCrab;
        EnemyCrushDie.OnEnemyHit -= audioManager.PlayKillCrab;
        EnemyCrushDie1.OnEnemyHit -= audioManager.PlayKillCrab;
        SeagullGrappleKill.OnEnemyHit -= audioManager.PlaySlamSeagull;
        PlayerHealth.OnDamageTaken -= audioManager.PlayHitByEnemy;
        PlayerHealth.PlayerKilledByEnemy -= audioManager.PlayDeath;
        LevelDeathCatcher.OnPlayerFellOutLevel -= audioManager.PlayDeath;
    }
}
