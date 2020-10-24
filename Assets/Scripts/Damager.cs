using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damageAmount;
    public void GiveDamage(Health health)
    {
        if(damageAmount < 0){
          Debug.LogError("damage amount is negative");
          return;  
        }
        health.GetDamage(damageAmount);
    }
}
