using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public void GetDamage(float amount)
    {
        if(amount < 0){
          Debug.LogError("damage amount is negative");
          return;  
        } 
        currentHealth -= amount;
        Debug.Log(currentHealth);
        currentHealth = currentHealth < 0 ? 0 : currentHealth > maxHealth ? maxHealth : currentHealth;
        Die();
    }
    public void GainHealth(float amount)
    {
        if(amount < 0){
          Debug.LogError("healing amount is negative");
          return;  
        };
        currentHealth += amount;
        currentHealth = currentHealth < 0 ? 0 : currentHealth > maxHealth ? maxHealth : currentHealth;
        Die();
    }
    void Die()
    {
        if(currentHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
