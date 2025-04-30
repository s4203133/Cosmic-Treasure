using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasiclinerenderScript : MonoBehaviour
{
    public GameObject targetObject1; // Reference to the first object
    public GameObject targetObject2; // Reference to the second object
    public LineRenderer lineRenderer; // Public variable to access the LineRenderer component

    void Start()
    {
        // Add a LineRenderer component if it doesn't exist
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
        }

        // Set the number of positions (points) to 2 for a line between two objects
        lineRenderer.positionCount = 2;

        // Set the positions of the line to the positions of the target objects
        lineRenderer.SetPosition(0, targetObject1.transform.position);
        lineRenderer.SetPosition(1, targetObject2.transform.position);
    }

    void Update()
    {
        // Update the line's positions to reflect changes in target object positions
        lineRenderer.SetPosition(0, targetObject1.transform.position);
        lineRenderer.SetPosition(1, targetObject2.transform.position);
    }
}