using LMO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkEnemy : MonoBehaviour
{
    public List<Vector3> Rays;
    public float distance;
    public NavMeshAgent Enemy;
    public List<Transform> PatrolPoints;
    private int PointIteration;
    private Transform CurrentPoint;
    public bool IsAtEnd;
    private float timer;
    public Animator animator;
    public GameObject SegualModel;

    public SpawnPlayer spawnplayer;
    public GameObject player;
    public float PlayerHealth;
    private float StartingPlayerHealth;
    private float currentPlayerHealth;
    private float timer2;
    public LayerMask layer;
    private bool canAttack;
    public float DamageAmount;
    public Rigidbody rb;

    private bool HasSeenPlayer;
    private float FindPlayerCountDown;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && canAttack == true) {
            PlayerHealth -= DamageAmount;
            if (PlayerHealth < currentPlayerHealth) {
                currentPlayerHealth = PlayerHealth;
                canAttack = false;
                
            }
        }
    }
    public void DoAttack() {
        if (Vector3.Distance(player.transform.position, Enemy.transform.position) <= 1 && canAttack == true) {
            //animator.SetBool("SharkAttack", false);
        }
        if (canAttack == false) {
            FindPlayerCountDown += Time.deltaTime;
            if (FindPlayerCountDown < 10) {
                Enemy.Move(new Vector3(player.transform.position.x, Enemy.transform.position.y, player.transform.position.z));
            }
            if (FindPlayerCountDown >= 10) {
                FindPlayerCountDown = 0;
                HasSeenPlayer = false;
                canAttack = true;
            }
        }
    }
    void RayDetectionUp() {
        foreach (Vector3 ray in Rays) {
            RaycastHit hit = new RaycastHit();
            Vector3 angle = Quaternion.Euler(ray.x, ray.y, ray.z) * Vector3.forward;
            Debug.DrawRay(transform.position, transform.TransformDirection(angle) * distance, Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(angle), out hit, distance, layer)) {
                Debug.Log("hit");
                if (hit.distance > 1) {
                    HasSeenPlayer = true;
                }
            }
        }
    }
    void Patrolling() {
        foreach (Transform point in PatrolPoints) {
            if (point == PatrolPoints[PointIteration]) {
                if (transform.position != point.transform.position) {
                    Enemy.SetDestination(point.transform.position);
                }
                CurrentPoint = point;
                if (Vector3.Distance(transform.position, CurrentPoint.position) <= 1) {
                    timer += Time.deltaTime;
                    animator.SetBool("SlimeIdle", true);
                    float rand = Random.Range(2, 7);
                    if (timer >= rand) {
                        PointIteration += 1;
                        timer = 0;
                        animator.SetBool("SlimeIdle", false);
                    }
                }
            }
        }
        if (PointIteration >= PatrolPoints.Count) {
            PointIteration = 0;
            IsAtEnd = false;
        }
    }
    // Update is called once per frame
    void Update() {
        if (HasSeenPlayer == false) {
            Patrolling();
        }
        if (HasSeenPlayer == true) {
            DoAttack();
        }
        RayDetectionUp();
        if (PlayerHealth <= 0) {
            PlayerHealth = StartingPlayerHealth;
            spawnplayer.ResetPlayer();
        }
        Debug.Log(PlayerHealth);
        if (canAttack == false) {
            timer2 += Time.deltaTime;
            if (timer2 >= 2) {
                canAttack = true;
                timer2 = 0;
            }
        }
    }
}
