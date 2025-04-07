using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using LMO;
using NR;


namespace WWH {
    public class Enemy1 : MonoBehaviour {
        public List<Vector3> Rays;
        public float distance;
        public NavMeshAgent Enemy;
        public List<Transform> PatrolPoints;
        private int PointIteration;
        private Transform CurrentPoint;
        private bool patrol;
        public bool IsAtEnd;
        private float timer;
        public Animator animator;
        public GameObject SlimeModel;
       // private bool CanSeePlayer;

        public SpawnPlayer spawnplayer;
        public GameObject player;
       // private float EnemyHealth;
        public float PlayerHealth;
        private float StartingPlayerHealth;
        private float currentPlayerHealth;
        private float timer2;
        public LayerMask layer;
        private bool canAttack;
        public float DamageAmount;
        
        // Start is called before the first frame update
        void Start() {
            IsAtEnd = false;
            canAttack = true;
            StartingPlayerHealth = PlayerHealth;
            currentPlayerHealth = PlayerHealth;
        }
        //private void OnTriggerEnter(Collider collision) {
        //    if (collision.CompareTag("Player")) {
        //        CanSeePlayer = true;
        //        Debug.Log("triggered");
        //    }
        //}
        //private void OnTriggerExit(Collider collision) {
        //    if (collision.CompareTag("Player")) {
        //        CanSeePlayer = false;
        //        Debug.Log("triggered");
        //    }
        //}
        
        void RayDirection() {
            foreach (Vector3 ray in Rays) {
                RaycastHit hit = new RaycastHit();
                Vector3 angle = Quaternion.Euler(ray.x, ray.y, ray.z) * Vector3.forward;
                Debug.DrawRay(transform.position, transform.TransformDirection(angle) * distance, Color.red);

                if (Physics.Raycast(transform.position, transform.TransformDirection(angle), out hit, distance, layer)) {
                    
                    //if (hit.collider.CompareTag("Player")) {
                        Debug.Log("hit");

                        if (hit.distance > 1) {
                            Enemy.SetDestination(hit.collider.transform.position);
                            transform.LookAt(hit.transform.position);

                            animator.SetBool("SlimeMoving", false);
                            //lerp to look at the player. if player out of sight get last location. if not there then reset
                        }
                        if (hit.distance <= 1 && canAttack == true) {
                            //play attack animation
                            PlayerHealth -= DamageAmount;
                            if (PlayerHealth < currentPlayerHealth) {
                                currentPlayerHealth = PlayerHealth;
                                canAttack = false;                                
                            }
                        }
                   // }
                }
            }
        }

        void Patrolling() {
            foreach (Transform point in PatrolPoints) {
                if (point == PatrolPoints[PointIteration]) {
                    if (transform.position != point.transform.position) {
                        Enemy.SetDestination(point.transform.position);
                        animator.SetBool("SlimeMoving", false);
                    }
                    CurrentPoint = point;
                    if (Vector3.Distance(transform.position, CurrentPoint.position) <= 1) {
                        timer += Time.deltaTime;
                        float rand = UnityEngine.Random.Range(2, 7);
                        if (timer >= rand) {
                            PointIteration += 1;
                            timer = 0;
                        }
                    }
                }
            }
            if (PointIteration >= PatrolPoints.Count) {
                PointIteration = 0;
                IsAtEnd = false;
            }
        }
        //private void Health() {
        //    //if (Spincollider.isTrigger) {
        //        //EnemyHealth -= 2;
        //        Debug.Log(EnemyHealth);

        //    if (EnemyHealth < 0) {
        //        Destroy(gameObject);
        //    }
        //    //}
        //}
        // Update is called once per frame
        void Update() {
            //if (CanSeePlayer == false) {
                Patrolling();
            //}
            //if (CanSeePlayer == true) {
                RayDirection();
            //}
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
}
