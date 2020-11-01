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
            var obj = damagesList.FirstOrDefault(s => s.damagedObject == health.gameObject);

            if(obj.damagedObject != null)
            {
                if(obj.damageTime + damageInterval > Time.time)
                {
                    return;
                }
                
                damagesList.Remove(obj);
            }
      }
        
        if(damageAmount < 0)
        {
          return;  
        }
        health.GetDamage(damageAmount);
        
        damagesList.Add(new Damages(health.gameObject, Time.time));
    }
}
