using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [Header("CORE MECHANICS")]
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource attack2;
    [SerializeField] private AudioSource land;
    [SerializeField] private AudioSource running;
    [SerializeField] private AudioSource groundPound;
    [SerializeField] private AudioSource groundPoundLand;
    [SerializeField] private AudioSource dive;
    [SerializeField] private AudioSource hover;

    [Header("SWINGING")]
    [SerializeField] private AudioSource shootRope;
    [SerializeField] private AudioSource swing;
    [SerializeField] private AudioSource releaseRope;
    [SerializeField] private AudioSource jumpFromRope;
    [SerializeField] private AudioSource fireSlingShot;

    [Header("COLLECTIBLES")]
    [SerializeField] private AudioSource collectCoin;
    [SerializeField] private AudioSource collectGem;

    [Header("INTERACTABLES")]
    [SerializeField] private AudioSource openChest;
    [SerializeField] private AudioSource hitLever;
    [SerializeField] private AudioSource hitButton;
    [SerializeField] private AudioSource breakGlassBottle;
    [SerializeField] private AudioSource openPage;

    [Header("ENEMIES")]
    [SerializeField] private AudioSource killCrab;
    [SerializeField] private AudioSource slamSeagull;
    [SerializeField] private AudioSource hitByEnemy;
    [SerializeField] private AudioSource death;


    [Header("CANNON")]
    [SerializeField] private AudioSource enterCannon;
    [SerializeField] private AudioSource launchCannon;

    private void PlaySound(AudioSource sound) {
        sound.pitch = Random.Range(0.88f, 1.12f);
        sound.Play();
    }

    public void PlayJump() => PlaySound(jump);
    public void PlayAttack() => PlaySound(attack);
    public void PlayAttack2() => PlaySound(attack2);
    public void PlayLand() => PlaySound(land);
    public void PlayRunning() => PlaySound(running);
    public void StopRunning() => running.Stop();
    public void PlayGroundPound() => PlaySound(groundPound);
    public void StopGroundPound() => groundPound.Stop();
    public void PlayGroundPoundLand() => PlaySound(groundPoundLand);
    public void PlayDive() => PlaySound(dive);
    public void PlayHover() => PlaySound(hover);
    public void StopHover() => hover.Stop();

    public void PlayShootRope() => PlaySound(shootRope);
    public void PlaySwing() => PlaySound(swing);
    public void PlayReleaseRope() => PlaySound(releaseRope);
    public void PlayJumpFromRope() => PlaySound(jumpFromRope);
    public void PlayFireSlingShot() => PlaySound(fireSlingShot);


    public void PlayCollectCoin() => PlaySound(collectCoin);
    public void PlayCollectGem() => PlaySound(collectGem);

    public void PlayOpenChest() => PlaySound(openChest);
    public void PlayHitLever() => PlaySound(hitLever);
    public void PlayHitButton() => PlaySound(hitButton);
    public void PlayBreakGlassBottle() => PlaySound(breakGlassBottle);
    public void PlayOpenPage() => PlaySound(openPage);

    public void PlayKillCrab() => PlaySound(killCrab);
    public void PlaySlamSeagull() => PlaySound(slamSeagull);
    public void PlayHitByEnemy() => PlaySound(hitByEnemy);
    public void PlayDeath() => PlaySound(death);


    public void PlayEnterCannon() => PlaySound(enterCannon);
    public void PlayLaunchCannon() => PlaySound(launchCannon);
}
