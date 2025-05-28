using LMO;
using UnityEngine;

public class PlayerIneractableEvents : MonoBehaviour, ICustomEvent
{
    private PlayerAudioManager audioManager;

    public void Initialise(EventManager manager) {
        PlayerEventManager player = manager as PlayerEventManager;
        audioManager = player.Controller.playerAudioManager;
    }

    public void SubscribeEvents() {

    }

    public void UnsubscribeEvents() {

    }
}
