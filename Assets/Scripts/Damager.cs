using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Damager : MonoBehaviour
{
    public Health mySelf;
    public float damageAmount;
    public float damageInterval = 1;
    public List<Damages> damagesList = new List<Damages>();
    [SerializeField]
    public struct Damages
    {
        public GameObject damagedObject;
        public float damageTime;
        public Damages(GameObject g, float t)
        {
            damagedObject = g;
            damageTime = t;
        }
    }
    public void GiveDamage(Health health)
    {
      if(health == mySelf) return;
      
      if(damagesList.Count > 0)
      {
          Debug.Log("in");
          var obj = damagesList.FirstOrDefault(s => s.damagedObject == health.gameObject);

          if(obj.damagedObject != null)
          {
              if(obj.damageTime + damageInterval > Time.time)
              {
                  Debug.Log(obj.damagedObject.name + " already damaged in 1 sec. lastDamageTime "+ obj.damageTime);
                  return;
              }
              Debug.Log(obj.damagedObject.name + " is okay to damage. lastDamageTime "+ obj.damageTime+"::: Time: "+ Time.time + ":::  " +(obj.damageTime + damageInterval < Time.time));
          }
      }
        
        if(damageAmount < 0)
        {
          Debug.LogError("damage amount is negative");
          return;  
        }
        health.GetDamage(damageAmount);
        
        damagesList.Add(new Damages(health.gameObject, Time.time));
        Debug.Log(health.gameObject.name + " added to list");
    }
}
