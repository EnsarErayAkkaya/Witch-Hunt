using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedZombie : BaseZombie
{
    public Animator animator;
    public float attackInterval;
    public float lastAttackTime;
    public Transform attackPos;
    public float attackAfter;
    public GameObject magicAttackPrefab;
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
            if(lastAttackTime + attackInterval < Time.time)
            {
                
                lastAttackTime = Time.time;
                animator.SetTrigger("attack");
                StartCoroutine( Shoot() );
            }
        }
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(attackAfter);
        Instantiate(magicAttackPrefab, attackPos.position, Quaternion.identity).GetComponent<MagicAttack>().Set(aiDestinationSetter.target.position);
    }
    public void Die()
    {
        spawner.RemoveEnemy(transform);
        Destroy(gameObject);
    }
}
