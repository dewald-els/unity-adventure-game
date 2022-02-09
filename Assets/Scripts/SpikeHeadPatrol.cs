using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadPatrol : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.0f;

    public Transform[] points;
    private int nextPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        Transform goal = points[nextPointIndex];

        // transform.position = Vector2.Lerp(transform.position, goal.position, moveSpeed * Time.fixedDeltaTime);
        transform.position = Vector2.MoveTowards(transform.position, goal.position, moveSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, goal.position) < 0.6)
        {
            Flip();
        }
        
    }

    private void Flip()
    {
        nextPointIndex++;
        if (nextPointIndex == points.Length)
        {
            nextPointIndex = 0;
        }
    }



}
