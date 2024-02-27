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
        animator = GetComponent<Animator>();
        aiPath.enabled = false;      
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
        if (target != null && target.gameObject.activeInHierarchy)
        {
            return; // Don't change the target if the current one is still alive
        }

        GameObject[] enemyUnits;
        enemyUnits = GameObject.FindGameObjectsWithTag("BlueUnit");

        foreach (GameObject enemyUnit in enemyUnits)
        {
            if (enemyUnit != null && enemyUnit.activeInHierarchy) // Check if alive
            {
                target = enemyUnit.transform;

                // Reset AIPath destination
                if (aiPath != null)
                {
                    aiPath.destination = target.position;
                    aiPath.isStopped = false;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsMoving", true);
                }

                return;
            }
        }
        target = null;
    }

    private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("BlueSword"))
            {
                
                Unit damagingUnit = other.GetComponentInParent<Unit>();
                if (damagingUnit != null)
                {
                    
                    TakeDamage(damagingUnit.damage, damagingUnit.gameObject);

                    //Debug.Log("Red unit took damage! Current health: " + health);
                }
            }
        }
}

