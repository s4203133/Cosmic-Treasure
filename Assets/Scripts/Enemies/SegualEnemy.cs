using LMO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WWH {
    public class SegualEnemy : MonoBehaviour, IResettable {
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
        private GameObject player;

        public LayerMask layer;
        public LayerMask Groundlayer;
        private bool canAttack;
        public float DamageAmount;
        private bool HasSeenPlayer;
        private float losingPlayerCountdown;
        private bool lostPlayer;
        private float FindPlayerCountDown;

        public GameObject Seagull;
        private Vector3 EnemyStartingPosition;

        void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
            canAttack = true;
            HasSeenPlayer = false;
            EnemyStartingPosition = Seagull.transform.position;
            CurrentPoint = PatrolPoints[0];
        }

        private void SeagullGroundCheck() {
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(SegualModelHolder.transform.position, transform.TransformDirection(Vector3.down) * 20, Color.red);
            if (Physics.Raycast(SegualModelHolder.transform.position, transform.TransformDirection(Vector3.down), out hit, 20, Groundlayer)) {

                if (hit.distance > 1 && canAttack == true) {
                    SegualModelHolder.transform.position -= new Vector3(0, 1, 0) * TimeValues.Delta;
                }
                if (canAttack == false) {
                    SegualModelHolder.transform.position += new Vector3(0, 1, 0) * TimeValues.Delta;
                }
            }
        }

        void RayDetectionDown() {
            if (!canAttack) {
                return;
            }
            foreach (Vector3 ray in Rays) {
                RaycastHit hit = new RaycastHit();
                Vector3 angle = Quaternion.Euler(ray.x, ray.y, ray.z) * Vector3.down;
                Debug.DrawRay(SegualModel.transform.position, transform.TransformDirection(angle) * distance, Color.red);
                if (Physics.Raycast(SegualModel.transform.position + Vector3.up, transform.TransformDirection(angle), out hit, distance, layer)) {
                    if (hit.distance > 0.1) {
                        SeenPlayer();
                        return;
                    }
                }
                LostPlayer();
            }
        }

        private void SeenPlayer() {
            HasSeenPlayer = true;
            FindPlayerCountDown = 0;
            animator.SetBool("Attacking", true);
            losingPlayerCountdown = 1f;
            lostPlayer = false;
        }

        private void LostPlayer() {
            losingPlayerCountdown -= TimeValues.Delta;
            if(losingPlayerCountdown <= 0 && !lostPlayer) {
                HasSeenPlayer = false;
                Enemy.isStopped = true;
                animator.SetBool("Attacking", false);
                lostPlayer = true;
            }
        }

        private void Fly() {
            if (HasSeenPlayer == true && canAttack == true) {
                //SeagullGroundCheck();
                SegualModel.transform.LookAt(player.transform.position);
                SegualModel.transform.position = Vector3.Lerp(SegualModel.transform.position, player.transform.position, 0.05f);
                if (Vector3.Distance(player.transform.position, SegualModel.transform.position) <= 0.1 && canAttack == true) {
                    canAttack = false;                    
                }
            }

            if (FindPlayerCountDown > 1) {
                SegualModel.transform.LookAt(new Vector3(CurrentPoint.transform.position.x, SegualModel.transform.position.y, CurrentPoint.transform.position.z));
                SegualModel.transform.position = Vector3.Lerp(SegualModel.transform.position, SegualModelHolder.transform.position, 0.07f);
                if (Vector3.Distance(SegualModel.transform.position, SegualModelHolder.transform.position) <= 0.1) {
                    canAttack = true;
                    Enemy.isStopped = false;
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
                        float rand = Random.Range(2, 7);
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

        public void Reset() {
            if (!this.enabled) {
                this.enabled = true;
                Enemy.isStopped = false;
                animator.SetBool("Attacking", false);
                animator.SetBool("Death", false);
            }

            if (!Seagull.activeInHierarchy) {
                Seagull.SetActive(true);
                Seagull.transform.position = EnemyStartingPosition;
            }
        }

        private void FixedUpdate() {
            Fly();
        }

        void Update() {
            if (HasSeenPlayer == false) {
                Patrolling();
                FindPlayerCountDown += TimeValues.Delta;
            }
            RayDetectionDown();

            //if (HasSeenPlayer == true && canAttack == false) {
            //    SeagullGroundCheck();
            //    FindPlayerCountDown += TimeValues.Delta;
            //    if (FindPlayerCountDown < 3) {
            //        Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, player.transform.position, 15 * TimeValues.Delta);
            //    }
            //    if (FindPlayerCountDown > 5) {
            //        FindPlayerCountDown = 0;
            //        canAttack = true;
            //        HasSeenPlayer = false;
            //    }
            //}
        }
    }
}
