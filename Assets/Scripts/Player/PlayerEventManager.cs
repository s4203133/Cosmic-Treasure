using UnityEngine;

public class PlayerEventManager : EventManager {

    [SerializeField] private PlayerRunEvent runEvent;
    [SerializeField] private PlayerJumpEvent jumpEvent;
    [SerializeField] private PlayerGroundedEvent groundedEvents;
    [SerializeField] private PlayerHighJumpEvent highJumpEvent;
    [SerializeField] private PlayerSpinEvent spinEvent;
    [SerializeField] private PlayerGroundPoundEvent groundPoundEvent;
    [SerializeField] private PlayerHoverEvent hoverEvent;
    [SerializeField] private PlayerDiveEvent diveEvent;

    private void Start() {
        SpawnPlayer.OnPlayerSpawned += SubscribeToPlayer;
    }

    private void SubscribeToPlayer(GameObject player) {
        SubscribeEvents();
    }

    protected override void SubscribeEvents() {
        runEvent.SubscribeEvents();
        jumpEvent.SubscribeEvents();
        groundedEvents.SubscribeEvents();
        highJumpEvent.SubscribeEvents();
        spinEvent.SubscribeEvents();
        groundPoundEvent.SubscribeEvents();
        hoverEvent.SubscribeEvents();
        diveEvent.SubscribeEvents();
    }

    protected override void UnsubscribeEvents() { 
        runEvent.UnsubscribeEvents();
        jumpEvent.UnsubscribeEvents();
        groundedEvents.UnsubscribeEvents();
        highJumpEvent.UnsubscribeEvents();
        spinEvent.UnsubscribeEvents();
        groundPoundEvent.UnsubscribeEvents();
        hoverEvent.UnsubscribeEvents();
        diveEvent.UnsubscribeEvents();
    }
}



public abstract class PlayerEvent {
    public abstract void SubscribeEvents();

    public abstract void UnsubscribeEvents();
}



[System.Serializable]
public class PlayerRunEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;

    public override void SubscribeEvents() {
        if (playerMovement == null) {
            return;
        }
        playerMovement.OnMoveStarted += playerVFX.StartRunParticles;
        playerMovement.OnMoveStopped += playerVFX.StopRunParticles;
    }

    public override void UnsubscribeEvents() {
        if (playerMovement == null) {
            return;
        }
        playerMovement.OnMoveStarted -= playerVFX.StartRunParticles;
        playerMovement.OnMoveStopped -= playerVFX.StopRunParticles;
    }
}



[System.Serializable]
public class PlayerJumpEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerJump playerJump;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;
    [SerializeField] private PlayerSquashAndStretch squishy;

    public override void SubscribeEvents() {
        if(playerJump == null) {
            return;
        }
        playerJump.OnJump += playerVFX.PlayJumpParticles;
        playerJump.OnJump += squishy.Jump.Play;
    }

    public override void UnsubscribeEvents() {
        if (playerJump == null) {
            return;
        }
        playerJump.OnJump -= playerVFX.PlayJumpParticles;
        playerJump.OnJump -= squishy.Jump.Play;
    }
}


[System.Serializable]
public class PlayerGroundedEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private Grounded playerGrounded;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;
    [SerializeField] private PlayerSquashAndStretch squishy;
    [SerializeField] private PlayerSpinAttack playerSpin;
    [SerializeField] private PlayerHover playerHover;

    public override void SubscribeEvents() {
        if (playerGrounded == null) {
            return;
        }
        playerGrounded.OnLanded += playerVFX.PlayLandParticles;
        playerGrounded.OnLanded += squishy.Land.Play;
        playerGrounded.OnLanded += playerSpin.ResetAirSpins;
        playerGrounded.OnLanded += playerHover.EnableHover;
    }

    public override void UnsubscribeEvents() {
        if (playerGrounded == null) {
            return;
        }
        playerGrounded.OnLanded -= playerVFX.PlayLandParticles;
        playerGrounded.OnLanded -= squishy.Land.Play;
        playerGrounded.OnLanded -= playerSpin.ResetAirSpins;
        playerGrounded.OnLanded -= playerHover.EnableHover;
    }
}



[System.Serializable]
public class PlayerHighJumpEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerJump playerJump;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;
    [SerializeField] private PlayerSquashAndStretch squishy;

    public override void SubscribeEvents() {
        if (playerJump == null) {
            return;
        }
        playerJump.OnHighJump += playerVFX.PlayJumpParticles;
        playerJump.OnHighJump += squishy.HighJump.Play;
    }

    public override void UnsubscribeEvents() {
        if (playerJump == null) {
            return;
        }
        playerJump.OnHighJump -= playerVFX.PlayJumpParticles;
        playerJump.OnHighJump -= squishy.HighJump.Play;
    }
}



[System.Serializable]
public class PlayerSpinEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerSpinAttack playerSpin;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;
    [SerializeField] private PlayerSquashAndStretch squishy;
    [SerializeField] private Animator animator;

    public override void SubscribeEvents() {
        if (playerSpin == null) {
            return;
        }
        playerSpin.OnSpin += playerVFX.PlaySpinVFX;
        playerSpin.OnSpin += squishy.SpinAttack.Play;
        playerSpin.OnSpin += AnimateSpin;
    }

    public override void UnsubscribeEvents() {
        if (playerSpin == null) {
            return;
        }
        playerSpin.OnSpin -= playerVFX.PlaySpinVFX;
        playerSpin.OnSpin -= squishy.SpinAttack.Play;
        playerSpin.OnSpin -= AnimateSpin;
    }

    private void AnimateSpin() {
        animator.SetTrigger("Spin");
    }
}



[System.Serializable]
public class PlayerGroundPoundEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerGroundPound playerGroundPound;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;
    [SerializeField] private PlayerSquashAndStretch squishy;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraShaker cameraShaker;

    public override void SubscribeEvents() {
        if (playerGroundPound == null) {
            return;
        }
        playerGroundPound.OnGroundPoundStarted += AnimateSpin;
        playerGroundPound.OnGroundPoundLanded += playerVFX.PlayGroundPoundParticles;
        playerGroundPound.OnGroundPoundLanded += squishy.GroundPound.Play;
        playerGroundPound.OnGroundPoundLanded += cameraShaker.shakeTypes.small.Shake;
    }

    public override void UnsubscribeEvents() {
        if (playerGroundPound == null) {
            return;
        }
        playerGroundPound.OnGroundPoundStarted -= AnimateSpin;
        playerGroundPound.OnGroundPoundLanded -= playerVFX.PlayGroundPoundParticles;
        playerGroundPound.OnGroundPoundLanded -= squishy.GroundPound.Play;
        playerGroundPound.OnGroundPoundLanded -= cameraShaker.shakeTypes.small.Shake;
    }

    private void AnimateSpin() {
        animator.SetTrigger("StartGroundPound");
    }
}

[System.Serializable]
public class PlayerHoverEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerHover playerHover;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;

    public override void SubscribeEvents() {
        if (playerHover == null) {
            return;
        }
        playerHover.OnHoverStarted += playerVFX.PlayHoverVFX;
        playerHover.OnHoverEnded += playerVFX.StopHoverVFX;
    }

    public override void UnsubscribeEvents() {
        if (playerHover == null) {
            return;
        }
        playerHover.OnHoverStarted -= playerVFX.PlayHoverVFX;
        playerHover.OnHoverEnded -= playerVFX.StopHoverVFX;
    }
}



[System.Serializable]
public class PlayerDiveEvent : PlayerEvent {
    [Header("SUBJECT")]
    [SerializeField] private PlayerDive playerDive;

    [Header("OBSERVERS")]
    [SerializeField] private PlayerVFX playerVFX;
    [SerializeField] private Animator animator;

    public override void SubscribeEvents() {
        if (playerDive == null) {
            return;
        }
        playerDive.OnDive += TriggerAnimation;
    }

    public override void UnsubscribeEvents() {
        if (playerDive == null) {
            return;
        }
        playerDive.OnDive -= TriggerAnimation;
    }

    private void TriggerAnimation() {
        animator.SetTrigger("Dive");
    }
}