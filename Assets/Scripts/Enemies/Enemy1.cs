using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LMO;
using Unity.VisualScripting;


namespace WWH {
    public class Enemy1 : MonoBehaviour, IResettable {
        
        public List<Vector3> Rays;
        public float distance;
        public NavMeshAgent Enemy;
        public List<Transform> PatrolPoints;
        private int PointIteration;
        private Transform CurrentPoint;
       
        private float timer;
        public Animator animator;
        public GameObject SlimeModel;

        public GameObject player;
        private float timer2;
        public LayerMask layer;
        private bool canAttack;
        public float DamageAmount;

        public GameObject Slime;
        private Vector3 EnemyStartingPosition;

        // Start is called before the first frame update
        void Start() {            
            canAttack = true;
            EnemyStartingPosition = Slime.transform.position;
        }
        
        void RayDirection() {
            foreach (Vector3 ray in Rays) {
                RaycastHit hit = new RaycastHit();
                Vector3 angle = Quaternion.Euler(ray.x, ray.y, ray.z) * Vector3.forward;
                Debug.DrawRay(transform.position, transform.TransformDirection(angle) * distance, Color.red);
                if (Physics.Raycast(transform.position, transform.TransformDirection(angle), out hit, distance, layer)) {
                        
                        if (hit.distance > 1) {
                            Enemy.SetDestination(hit.collider.transform.position);
                            transform.LookAt(hit.transform.position);
                        }
                    if (hit.distance <= 1 && canAttack == true) {
                        //play attack animation
                        animator.SetBool("SlimeAttack", true);                        
                        animator.SetBool("SlimeIdle", false);                       
                        
                        if (animator.GetBool("SlimeAttack")) {                            
                            canAttack = false;
                            animator.SetBool("SlimeAttack", false);                    
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
            }            
        }
        public void Reset() {
            
            if (!Slime.activeInHierarchy) {
                Slime.SetActive(true);
                Slime.transform.position = EnemyStartingPosition;
            }
        }
        // Update is called once per frame
        void Update() {            
                Patrolling();
                RayDirection();           
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
