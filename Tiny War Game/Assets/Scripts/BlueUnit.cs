using UnityEngine;
using Pathfinding;
using System.Collections;


public class BlueUnit : Unit
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
                return;
            }   

        GameObject[] enemyUnits;
        enemyUnits = GameObject.FindGameObjectsWithTag("RedUnit");

        foreach (GameObject enemyUnit in enemyUnits)
        {
            if (enemyUnit != null && enemyUnit.activeInHierarchy)
            {
                target = enemyUnit.transform;

                
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
       
        if (other.CompareTag("RedSword"))
        {
            
            Unit damagingUnit = other.GetComponentInParent<Unit>();
            if (damagingUnit != null)
            {
                
                TakeDamage(damagingUnit.damage, damagingUnit.gameObject);;
                
                //Debug.Log("Blue unit took damage! Current health: " + health);
            }
        }
    }

}

