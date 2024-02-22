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
    }

    public void TakeDamage(int amount)
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
            Die();
        }

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
        UpdateHealthText();
    }

    protected virtual void Die()
    {
        
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        StartCoroutine(DestroyAfterAnimation());
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
