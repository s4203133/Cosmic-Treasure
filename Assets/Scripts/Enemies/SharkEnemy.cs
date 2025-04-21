using LMO;
using System.Collections.Generic;
using UnityEngine;

public class SharkEnemy : MonoBehaviour
{
    public List<Vector3> Rays;
    public float distance;
    public GameObject Enemy;
    public List<Transform> PatrolPoints;
    private int PointIteration;
    private Transform CurrentPoint;
    
    private float timer;
    public Animator animator;
    public GameObject SharkModel;

    public SpawnPlayer spawnplayer;
    public GameObject player;
    public float PlayerHealth;
    private float StartingPlayerHealth;
    private float currentPlayerHealth;
    private float timer2;
    public LayerMask layer;
    public bool canAttack;
    public float DamageAmount;
   

    private bool HasSeenPlayer;
    private float AttackCooldown;

    private void Start() {
        canAttack = true;
    }

    public void DoAttack() {
        if (Vector3.Distance(player.transform.position, Enemy.transform.position) <= 5 && canAttack == true) {
            //animator.SetBool("SharkAttack", true);
        }        
    }
    void RayDetectionUp() {
        foreach (Vector3 ray in Rays) {
            RaycastHit hit = new RaycastHit();
            Vector3 angle = Quaternion.Euler(ray.x, ray.y, ray.z) * Vector3.up;
            Debug.DrawRay(SharkModel.transform.position, transform.TransformDirection(angle) * distance, Color.red);
            if (Physics.Raycast(SharkModel.transform.position, transform.TransformDirection(angle), out hit, distance, layer)) {
                Debug.Log("hit");
                    HasSeenPlayer = true;                
            }
        }
    }
    void Patrolling() {
        foreach (Transform point in PatrolPoints) {
            if (point == PatrolPoints[PointIteration]) {
                if (transform.position != point.transform.position) {                    
                    Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, point.transform.position, 10 * Time.deltaTime);
                    Enemy.transform.LookAt(point.transform.position);
                }
                CurrentPoint = point;
                if (Vector3.Distance(transform.position, CurrentPoint.position) <= 1) {
                    timer += Time.deltaTime;                    
                    float rand = 0.5f;
                    if (timer >= rand) {
                        PointIteration += 1;
                        timer = 0;                       
                    }
                }
            }
        }
        if (PointIteration >= PatrolPoints.Count) {
            PointIteration = 0;            
        }
    }
    // Update is called once per frame
    void Update() {
        
        Patrolling();        
        if (HasSeenPlayer == true) {
            DoAttack();
        }
        RayDetectionUp();
        if (PlayerHealth <= 0) {
            PlayerHealth = StartingPlayerHealth;
            spawnplayer.ResetPlayer();
        }        
        if (canAttack == false) {
            AttackCooldown += Time.deltaTime;
            if (AttackCooldown >= 5) {
                AttackCooldown = 0;                
                canAttack = true;
            }
        }
    }
}
