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


        private float timer;
        public Animator animator;
        public GameObject SegualModel;
        public GameObject SegualModelHolder;

        public SpawnPlayer spawnplayer;
        public GameObject player;
        public float PlayerHealth;
        private float StartingPlayerHealth;
        private float currentPlayerHealth;
        private float timer2;
        public LayerMask layer;
        public LayerMask Groundlayer;
        private bool canAttack;
        public float DamageAmount;
        private bool HasSeenPlayer;
        private float FindPlayerCountDown;

        // Start is called before the first frame update
        void Start() {
            canAttack = true;
            StartingPlayerHealth = PlayerHealth;
            currentPlayerHealth = PlayerHealth;
            HasSeenPlayer = false;
        }
        private void SeagullGroundCheck() {
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(SegualModelHolder.transform.position, transform.TransformDirection(Vector3.down) * 20, Color.red);
            if (Physics.Raycast(SegualModelHolder.transform.position, transform.TransformDirection(Vector3.down), out hit, 20, Groundlayer)) {

                if (hit.distance > 1 && canAttack == true) {
                    SegualModelHolder.transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
                }
                if (/*hit.distance < 20 && */canAttack == false) {
                    SegualModelHolder.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
                }
            }
        }
        void RayDetectionDown() {
            foreach (Vector3 ray in Rays) {
                RaycastHit hit = new RaycastHit();
                Vector3 angle = Quaternion.Euler(ray.x, ray.y, ray.z) * Vector3.down;
                Debug.DrawRay(SegualModel.transform.position, transform.TransformDirection(angle) * distance, Color.red);
                if (Physics.Raycast(SegualModel.transform.position, transform.TransformDirection(angle), out hit, distance, layer)) {
                    if (hit.distance > 1) {
                        HasSeenPlayer = true;
                    }
                }
            }
        }
        private void FlyAway() {
            if (HasSeenPlayer == true && canAttack == true) {
                SeagullGroundCheck();
                Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, player.transform.position, 10 * Time.deltaTime);
                //SegualModel.transform.LookAt(player.transform);

                //Mathf.Lerp(player.transform.position.x, transform.position.x, Time.deltaTime);
                if (Vector3.Distance(player.transform.position, SegualModel.transform.position) <= 2.5 && canAttack == true) {
                    PlayerHealth -= DamageAmount;
                    if (PlayerHealth < currentPlayerHealth) {
                        currentPlayerHealth = PlayerHealth;
                        canAttack = false;
                        // animator.SetBool("SlimeAttack", false);
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
                        //animator.SetBool("SlimeIdle", true);
                        float rand = Random.Range(2, 7);
                        if (timer >= rand) {
                            PointIteration += 1;
                            timer = 0;
                            // animator.SetBool("SlimeIdle", false);
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

            Debug.Log(FindPlayerCountDown);
            FlyAway();
            if (HasSeenPlayer == false) {
                Patrolling();
            }
            RayDetectionDown();
            if (PlayerHealth <= 0) {
                PlayerHealth = StartingPlayerHealth;
                spawnplayer.ResetPlayer();
            }

            if (HasSeenPlayer == true && canAttack == false) {
                SeagullGroundCheck();
                

                FindPlayerCountDown += Time.deltaTime;
                if (FindPlayerCountDown < 5) {
                    Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, player.transform.position, 10 * Time.deltaTime);
                }
                if (FindPlayerCountDown > 5) {
                    FindPlayerCountDown = 0;

                    canAttack = true;
                    HasSeenPlayer = false;
                }
                
            }

        }
    }
}
