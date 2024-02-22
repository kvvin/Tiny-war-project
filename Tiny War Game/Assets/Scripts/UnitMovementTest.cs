using UnityEngine;
using Pathfinding;
using System.Collections;

public class UnitMovementTest : MonoBehaviour
{
    private Seeker seeker;
    private CharacterController controller;
    private Transform target;
    private Animator animator; 
    public Path path;
    public float speed = 2;
    public float stoppingDistance = 0.5f;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public bool reachedEndOfPath;
    private Coroutine movementCoroutine;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            // Start a new path to the target position
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }


    // Coroutine to start movement after a delay
    IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject[] redUnits = GameObject.FindGameObjectsWithTag("RedUnit");

        // Check if any red units are found
        if (redUnits.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            foreach (GameObject redUnit in redUnits)
            {
                float distance = Vector3.Distance(transform.position, redUnit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = redUnit.transform;
                }
            }

            // Check if a target was successfully found
            if (target != null)
            {
                seeker.StartPath(transform.position, target.position, OnPathComplete);
            }
            else
            {
                Debug.LogError("Failed to find target.");
            }
        }
        else
        {
            Debug.LogError("No RedUnit found in the scene.");
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            // If coroutine is already running, stop it
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);

            // Start the coroutine with a delay of 5 seconds
            movementCoroutine = StartCoroutine(StartMovementAfterDelay(5f));
        }

        // Continue updating movement if a path is available
        if (path != null)
{
    reachedEndOfPath = false;
    float distanceToWaypoint;
    while (true)
    {
        distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distanceToWaypoint < nextWaypointDistance)
        {
            if (currentWaypoint + 1 < path.vectorPath.Count)
                currentWaypoint++;
            else
            {
                reachedEndOfPath = true;
                break;
            }
        }
        else
            break;
    }

    var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;
    Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
    Vector3 velocity = dir * speed * speedFactor;

    velocity.y = 0f;

    controller.SimpleMove(velocity);

    // Rotate the unit towards the target
    if (target != null)
    {
        Vector3 lookDirection = (target.position - transform.position).normalized;
        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        }
    }

    // If the unit reached the end of the path
    if (distanceToWaypoint <= stoppingDistance)
    {
        // Trigger the attack animation
        animator.SetBool("IsAttack", true);
        // Stop movement
        path = null;
        currentWaypoint = 0;
        return;
    }
}
        bool isMoving = !reachedEndOfPath && target != null;
        animator.SetBool("IsMoving", isMoving);
        
        
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}

