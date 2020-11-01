using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    Player p;
    private void Start() {
        p =  GetComponent<Player>();
    }
    public void GetDamage(float amount)
    {
        if(amount < 0){
          Debug.LogError("damage amount is negative");
          return;  
        }
        if(p != null && p.movement.stop == true)
            return;
        else
        {
            currentHealth -= amount;
        }
        if(p != null )
        {
            p.CallHealthUIUpdate(currentHealth);
        }
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
        if( GetComponent<Player>() != null )
        {
            GetComponent<Player>().CallHealthUIUpdate(currentHealth);
        }
        currentHealth = currentHealth < 0 ? 0 : currentHealth > maxHealth ? maxHealth : currentHealth;
        Die();
    }
    void Die()
    {
        if(currentHealth <= 0)
        {
            if( GetComponent<Zombie>() != null )
            {
                GetComponent<Zombie>().Die();
            }
            else if( GetComponent<RangedZombie>() != null )
            {
                GetComponent<RangedZombie>().Die();
            }
            else if( GetComponent<Player>() != null )
            {
                GetComponent<Player>().Die();
            }
        }
    }
}
