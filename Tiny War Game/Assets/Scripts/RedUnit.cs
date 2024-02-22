using UnityEngine;
using Pathfinding;
using System.Collections;

public class RedUnit : Unit
{
    private AIPath aiPath; 
    private Transform target;
    private bool delayComplete = false;

    protected override void Start()
    {
        base.Start();
        aiPath = GetComponent<AIPath>(); 
        aiPath.enabled = false; 
        animator = GetComponent<Animator>(); 
        StartCoroutine(StartMovementAfterDelay(5f));

    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        delayComplete = true; 
        SetNewTarget();
        aiPath.enabled = true;
    }

    
    void Update()
    {
        if (!delayComplete)
            return; 

        if (target != null)
        {
            aiPath.destination = target.position; 
        }

        if (aiPath.reachedEndOfPath)
        {
            
            animator.SetBool("IsAttack", true);
            aiPath.isStopped = true;
        }

        animator.SetBool("IsMoving", aiPath.velocity.magnitude > 0);
        if (target == null)
        {
            SetNewTarget();
        }
    }

    private void SetNewTarget()
    {
        GameObject[] blueUnits = GameObject.FindGameObjectsWithTag("BlueUnit");
        foreach (GameObject blueUnit in blueUnits)
        {
            if (blueUnit != null)
            {
                target = blueUnit.transform;
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("BlueSword"))
            {
                
                Unit damagingUnit = other.GetComponentInParent<Unit>();
                if (damagingUnit != null)
                {
                    
                    TakeDamage(damagingUnit.damage);

                    //Debug.Log("Red unit took damage! Current health: " + health);
                }
            }
        }
}

