using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : BaseZombie
{
    public Animator animator;
    public float attackInterval;
    public float lastAttackTime;
    public EnemySpawner spawner;
    
    public void Set(EnemySpawner _spawner)
    {
        spawner = _spawner;
    }
    private void Update() 
    {
        if(stopped) return;
        animator.SetFloat("speed", aIPath.velocity.magnitude);
        if(aIPath.reachedDestination)
        {
            transform.LookAt(aiDestinationSetter.target);
            if( lastAttackTime + attackInterval < Time.time)
            {
                
                lastAttackTime = Time.time;
                int i = Random.Range(0,2);
                if(i == 0)
                    animator.SetTrigger("attack1");
                else
                    animator.SetTrigger("attack2");
            }
        }
    }
    
    public void Die()
    {
        AudioManager.instance.Play("Die");
        spawner.RemoveEnemy(transform);
        Destroy(gameObject);
    }

}
