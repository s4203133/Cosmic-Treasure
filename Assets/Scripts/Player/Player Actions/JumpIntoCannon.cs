using System;
using UnityEngine;

public class JumpIntoCannon : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cannonTransform;

    [Space(15)]
    [SerializeField] private Animator cannonAnimator;

    private bool isJumping;

    public Action OnPlayerEnteredCannnon;

    private void FixedUpdate()
    {
        if (isJumping)
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, cannonTransform.position, 0.1f);
        }
    }

    public void Play()
    {
        playerAnimator.SetTrigger("CannonJump");
        isJumping = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isJumping && other.tag == "Player")
        {
            playerTransform.gameObject.SetActive(false);
            cannonAnimator.SetTrigger("Interact");
            OnPlayerEnteredCannnon?.Invoke();
            isJumping = false;
        }
    }
}
