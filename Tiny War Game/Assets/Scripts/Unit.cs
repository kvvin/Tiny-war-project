using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public int damage = 10;
    protected Animator animator;
    protected Slider healthSlider;
    protected Text healthText;
    public EventsPanelManager eventsPanelManager;
    

    protected virtual void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        healthSlider = GetComponentInChildren<Slider>();
        healthText = GetComponentInChildren<Text>(); 
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
            
        }
        UpdateHealthText();
        eventsPanelManager = FindObjectOfType<EventsPanelManager>();
    }

    public void TakeDamage(int amount, GameObject damagingUnit)
    {
        health -= amount;
        if (animator != null)
        {
            animator.SetTrigger("Damage");
            if(health != 0){
                animator.SetBool("IsAttack",true);
            }
        }
        if (health == 0)
        {
            health = 0;
            Die(damagingUnit);
        }

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
        UpdateHealthText();
    }

    protected virtual void Die(GameObject damagingUnit)
    {
        
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        StartCoroutine(DestroyAfterAnimation());
        if (eventsPanelManager != null)
        {
            if (gameObject.name.StartsWith("BlueUnit_"))
            {
                eventsPanelManager.SpawnBlueBubble(damagingUnit.name, "killed " + gameObject.name);
                eventsPanelManager.UpdateScore("Red", 10);
            }
            else if (gameObject.name.StartsWith("RedUnit_"))
            {
                eventsPanelManager.SpawnRedBubble(damagingUnit.name, "killed " + gameObject.name);
                eventsPanelManager.UpdateScore("Blue", 10); 
            }
        }
    }

    protected IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

     protected void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }
}
