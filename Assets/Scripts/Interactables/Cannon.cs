using LMO;
using System;
using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Animator promptUIAnimator;
    [SerializeField] private JumpIntoCannon jumpInCannon;
    [SerializeField] private LaunchCannon launchCannon;
    private BoxCollider boxCollision;

    private bool playerInRange;

    [Header("Launching Cannon")]
    private WaitForSeconds launchDelay;
    [SerializeField] private float launchDelayTime;

    public static Action OnLevelEnded;
    public static Action OnCannonLaunched;

    private bool hasBeenInteractedWith;

    private void Start()
    {
        boxCollision = GetComponent<BoxCollider>();
        launchDelay = new WaitForSeconds(launchDelayTime);
    }

    private void OnEnable()
    {
        jumpInCannon.OnPlayerEnteredCannnon += LaunchCannon;
        InputHandler.jumpPerformed += JumpIntoCannon;
    }

    private void OnDisable()
    {
        jumpInCannon.OnPlayerEnteredCannnon -= LaunchCannon;
        InputHandler.jumpPerformed -= JumpIntoCannon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
            promptUIAnimator.SetTrigger("FadeIn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = false;
            promptUIAnimator.SetTrigger("FadeOut");
        }
    }

    private void JumpIntoCannon()
    {
        if (!playerInRange || hasBeenInteractedWith) 
        { 
            return; 
        }
        OnLevelEnded?.Invoke();
        jumpInCannon.Play();
        boxCollision.enabled = false;
        promptUIAnimator.SetTrigger("FadeOut");
        hasBeenInteractedWith = true;
    }

    private void LaunchCannon()
    {
        StartCoroutine(LaunchPlayerOutCannon());    
    }

    private IEnumerator LaunchPlayerOutCannon()
    {
        yield return launchDelay;
        launchCannon.Launch();
        OnCannonLaunched?.Invoke();
    }
}
