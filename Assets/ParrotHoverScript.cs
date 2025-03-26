using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParrotHoverScript : MonoBehaviour
{
    public GameObject companionAnchor;
    //public float speed = 0.3f; // how fast it'll catch up, 0.3 seconds
    private Rigidbody rb;

    Vector3 refVel = Vector3.zero;
    public float smoothVal = .2f; // Higher = 'Smoother'

    void Start()
    {
        companionAnchor = GameObject.Find("companionAnchor");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, companionAnchor.transform.position);

        if (dist >= 2.0f)
        {
            rb.MovePosition(Vector3.SmoothDamp(transform.position, companionAnchor.transform.position, ref refVel, smoothVal));

        }
    }
}