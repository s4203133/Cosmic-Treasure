using UnityEngine;

public class PlayerEventManager : EventManager {

    [SerializeField] private PlayerJumpEvent jumpEvent;
    [SerializeField] private PlayerHighJumpEvent highJumpEvent;
    [SerializeField] private PlayerSpinEvent spinEvent;
    [SerializeField] private PlayerGroundPoundEvent groundPoundEvent;

    protected override void SubscribeEvents() {
        jumpEvent.SubscribeEvents();
        highJumpEvent.SubscribeEvents();
        spinEvent.SubscribeEvents();
        groundPoundEvent.SubscribeEvents();
    }

    protected override void UnsubscribeEvents() { 
        jumpEvent.UnsubscribeEvents();
        highJumpEvent.UnsubscribeEvents();
        spinEvent.UnsubscribeEvents();
        groundPoundEvent.UnsubscribeEvents();
    }
}



public abstract class PlayerEvent {
    public abstract void SubscribeEvents();

    public abstract void UnsubscribeEvents();
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
