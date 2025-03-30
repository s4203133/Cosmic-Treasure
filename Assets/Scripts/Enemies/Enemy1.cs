using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public List<Vector3> Rays;
    public float distance;
    public NavMeshAgent Enemy;
    public List<Transform> PatrolPoints;
    private int PointIteration;
    private Transform CurrentPoint;
    private bool patrol;
    public bool IsAtEnd;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        IsAtEnd = false;
    }
    void RayDirection() {
        foreach (Vector3 ray in Rays) {
            RaycastHit hit = new RaycastHit();
            Vector3 angle = new Vector3(0, 0, 0) + Quaternion.Euler(ray.x, ray.y, ray.z) * transform.forward;
            Debug.DrawRay(transform.position, transform.TransformDirection(angle) * distance, Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(angle), out hit, distance)) {
                
                if (hit.collider.CompareTag("Player")) {
                    if (hit.distance > 1) {
                        Enemy.SetDestination(hit.collider.transform.position);
                        transform.LookAt(hit.transform.position);
                        //lerp to look at the player. if player out of sight get last location. if not there then reset
                    }
                    if (hit.distance <= 1) {
                        //play attack animation
                    }


                    
                }
            }
            else {
                foreach (Transform point in PatrolPoints) {
                    if (point == PatrolPoints[PointIteration]) {
                        if (transform.position != point.transform.position) {
                            Enemy.SetDestination(point.transform.position);

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
                if (PointIteration >= PatrolPoints.Count ) {
                    PointIteration = 0;
                    IsAtEnd = false;
                }
            }
            Debug.Log(timer);
        }
    }
    // Update is called once per frame
    void Update()
    {
        RayDirection();

    }
}
