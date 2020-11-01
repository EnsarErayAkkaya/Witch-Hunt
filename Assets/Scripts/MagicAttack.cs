using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    private Damager damager;
    public Rigidbody rb;
    public Vector3 target;
    public float speed;
    public GameObject impactPrefab;
    public void Set(Vector3 _target)
    {
        AudioManager.instance.Play("magicAttackStart");
        this.target = _target;
        target.y += 1.2f;
        damager = GetComponent<Damager>();
        transform.LookAt(target);
        rb.velocity = (target - transform.position) * speed;
        Destroy(gameObject, 4); 
    }
    private void Update() 
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if( other.CompareTag("Player") && other.GetComponent<Health>() )
        {
            AudioManager.instance.Play("magicAttackDestroy");
            GameObject impactVFX = Instantiate(impactPrefab,transform.position,Quaternion.Euler(new Vector3(0,-90,0)));
            Destroy(impactVFX,3);
            damager.GiveDamage(other.GetComponent<Health>());
            Destroy(gameObject); 
        }
        else if( other.CompareTag("Ground") || other.CompareTag("Env") )
        {
            AudioManager.instance.Play("magicAttackDestroy");
            GameObject impactVFX = Instantiate(impactPrefab,transform.position,Quaternion.Euler(new Vector3(0,-90,0)));
            Destroy(impactVFX,3);
            Destroy(gameObject); 
        }
    }
}
