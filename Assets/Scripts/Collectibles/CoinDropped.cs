using LMO;
using System.Collections;
using UnityEngine;

public class CoinDropped : Coin
{
    private Rigidbody rigidBody;
    private MeshRenderer mesh;
    private Animator animator;

    private bool hasDropped;
    private bool hasBounced;

    private WaitForSeconds registerDropDelay;

    public void Initialse()
    {
        rigidBody = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<MeshRenderer>();
        animator = GetComponent<Animator>();

        mesh.enabled = false;
        rigidBody.isKinematic = true;

        hasDropped = false;
        hasBounced = false;
        registerDropDelay = new WaitForSeconds(0.5f);
    }

    public void Drop()
    {
        mesh.enabled = true;
        rigidBody.isKinematic = false;
        StartCoroutine(SetDropped());
    }

    private IEnumerator SetDropped()
    {
        yield return registerDropDelay;
        hasDropped = true;
    }

    protected override void HitGround()
    {
        if (!hasDropped || hasBounced)
        {
            return;
        }
        rigidBody.isKinematic = true;
        animator.SetTrigger("Bounce");
        hasBounced = true;
    }
}
