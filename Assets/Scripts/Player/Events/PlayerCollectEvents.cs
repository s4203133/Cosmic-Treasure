using LMO;
using UnityEngine;

public class PlayerCollectEvents : MonoBehaviour, ICustomEvent {
    // Observers
    private PlayerAudioManager audioManager;

    public void Initialise(EventManager manager) {
        PlayerEventManager player = manager as PlayerEventManager;
        audioManager = player.Controller.playerAudioManager;
    }

    public void SubscribeEvents() {
        Coin.OnCollected += CollectCoin;
        Gem.OnGemCollected += CollectGem;
    }

    public void UnsubscribeEvents() {
        Coin.OnCollected -= CollectCoin;
        Gem.OnGemCollected -= CollectGem;
    }

    private void CollectCoin() {
        audioManager.PlayCollectCoin();
    }

    private void CollectGem() {
        audioManager.PlayCollectGem();
    }
}
