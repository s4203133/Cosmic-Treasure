using LMO;
using UnityEngine;
using UnityEngine.AI;
using WWH;

public class SeagullGrappleKill : GrapplePoint, IResettable
{
    public GameObject Seagull;
    private NavMeshAgent agent;
    private Animator anim;
    private SegualEnemy seagull;

    protected override void Start() {
        base.Start();
        agent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        seagull = GetComponentInParent<SegualEnemy>();
    }

    public override void OnReleased() {
        agent.isStopped = true;
        anim.SetBool("Death", true);
        seagull.enabled = false;
        canConnect = false;
    }

    public void Reset() {
        canConnect = true;
    }
}
