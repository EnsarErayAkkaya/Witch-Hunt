using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damageAmount;
    public float damageInterval = 1;
    private float lastDamageTime;
    public void GiveDamage(Health health)
    {
      if(lastDamageTime + damageInterval > Time.time) return;

      lastDamageTime = Time.time;

        if(damageAmount < 0){
          Debug.LogError("damage amount is negative");
          return;  
        }
        health.GetDamage(damageAmount);
    }
}
