using LMO;
using UnityEngine;
using UnityEngine.AI;
using WWH;

public class SeagullGrappleKill : GrapplePoint, IResettable
{
    private Transform thisTransform;
    private NavMeshAgent agent;
    private Animator anim;
    private SegualEnemy seagull;

    private GameObject player;

    private bool killed;
    private bool hitGround;

    [SerializeField] private float slamSpeed;
    [SerializeField] private float flyAwaySpeed;
    private Vector3 flyAwayDirection;

    protected override void Start() {
        base.Start();
        thisTransform = transform;
        agent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        seagull = GetComponentInParent<SegualEnemy>();
        player = GameObject.FindWithTag("Player");
    }

    public override void OnReleased() {
        agent.isStopped = true;
        anim.SetBool("Death", true);
        seagull.enabled = false;
        canConnect = false;
        killed = true;
        hitGround = false;
    }

    public void Reset() {
        canConnect = true;
        hitGround = false;
        killed = false;
    }

    private void Update() {
        if (killed) {
            Vector3 flyDirection;
            float speed;
            if (!hitGround) {
                speed = slamSpeed;
                flyDirection = Vector3.down;
            }
            else {
                speed = flyAwaySpeed;
                flyDirection = flyAwayDirection + (Vector3.up * 0.75f);
            }
            thisTransform.Translate(flyDirection * speed * TimeValues.Delta);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(killed) {
            if(other.gameObject.layer == 6) {
                hitGround = true;
                flyAwayDirection = (thisTransform.position - player.transform.position).normalized;
            }
        }
    }
}
