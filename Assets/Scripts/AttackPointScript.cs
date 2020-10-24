using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointScript : MonoBehaviour
{
    private Damager damager;
    private Collider collider;
    private void Start() {
        damager = GetComponent<Damager>();
        collider =  GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if( other.GetComponent<Health>() )
        {
            damager.GiveDamage(other.GetComponent<Health>());
            collider.enabled = false;
        }
    }
}
