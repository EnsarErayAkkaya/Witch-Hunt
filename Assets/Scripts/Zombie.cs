using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Animator animator;
    public AIPath aIPath;
    public float attackInterval;
    public float lastAttackTime;

    private void Update() {
        animator.SetFloat("speed", aIPath.velocity.magnitude);
        if(aIPath.reachedDestination && lastAttackTime + attackInterval < Time.time)
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
