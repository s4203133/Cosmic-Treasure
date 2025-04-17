using LMO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace WWH {
    public class SegualEnemy : MonoBehaviour {
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

        // Start is called before the first frame update
        void Start() {
            IsAtEnd = false;
            canAttack = true;
            StartingPlayerHealth = PlayerHealth;
            currentPlayerHealth = PlayerHealth;
            HasSeenPlayer = false;
        }

        void RayDetectionDown() {
            foreach (Vector3 ray in Rays) {
                RaycastHit hit = new RaycastHit();
                Vector3 angle = Quaternion.Euler(ray.x, ray.y, ray.z) * Vector3.forward;
                Debug.DrawRay(transform.position, transform.TransformDirection(angle) * distance, Color.red);
                if (Physics.Raycast(transform.position, transform.TransformDirection(angle), out hit, distance, layer)) {
                    Debug.Log("hit");
                    if (hit.distance > 1) {
                                               
                        HasSeenPlayer = true;
                        
                        //lerp to look at the player. if player out of sight get last location. if not there then reset
                    }
                    //if (hit.distance <= 1 && canAttack == true) {
                    //    //play attack animation
                    //    animator.SetBool("SlimeAttack", true);

                    //    animator.SetBool("SlimeIdle", false);
                    //    PlayerHealth -= DamageAmount;

                    //    rb.AddForce(transform.up * 1000);
                    //    if (PlayerHealth < currentPlayerHealth) {
                    //        currentPlayerHealth = PlayerHealth;
                    //        canAttack = false;
                    //        animator.SetBool("SlimeAttack", false);

                    //        //animator.SetBool("SlimeAttack", false);
                    //    }
                    //}
                }
            }
        }
        private void FlyAway() {
            if (HasSeenPlayer == true && canAttack == true) {
                Enemy.SetDestination(player.transform.position);
                //transform.LookAt(hit.transform.position);
                Mathf.Lerp(player.transform.position.x, transform.position.x, Time.deltaTime);
                if (Vector3.Distance(player.transform.position, Enemy.transform.position) <= 1 && canAttack == true) {
                    PlayerHealth -= DamageAmount;                    
                    if (PlayerHealth < currentPlayerHealth) {
                        currentPlayerHealth = PlayerHealth;
                        canAttack = false;
                        animator.SetBool("SlimeAttack", false);
                    }
                }
                if (canAttack == false) {
                    
                    if (Enemy.transform.position.y < 20) {
                        Enemy.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
                    }
                    if (Enemy.transform.position.y >= 20) {
                        FindPlayerCountDown += Time.deltaTime;
                        if (FindPlayerCountDown >= 10) {
                            Enemy.Move(new Vector3(player.transform.position.x, Enemy.transform.position.y, player.transform.position.z));
                            FindPlayerCountDown = 0;
                        }
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
            Patrolling();
            RayDetectionDown();
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
