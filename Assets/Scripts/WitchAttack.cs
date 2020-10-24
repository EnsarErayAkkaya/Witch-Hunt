using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAttack : MonoBehaviour
{
    public Player player;
    public float normalAttackSpeed;
    float currentAttackSpeed;
    public AttackType[] attackTypes;
    public AttackType nextAttackType;
    public bool willAttack, cancelAttackTimer;
    public GameObject meteor,iceShard;

    private void Start() 
    {
        currentAttackSpeed = normalAttackSpeed;
        ChooseNextAttack();
        StartCoroutine( AttackRoutine() );
    }

    IEnumerator AttackRoutine()
    {
        float t = 0;
        while (t < currentAttackSpeed)
        {
            if(willAttack)
            {
                t += Time.deltaTime;
            }
            else if( !willAttack && cancelAttackTimer )
            {
                break;
            }
            yield return null;
        }
        if( !cancelAttackTimer )
        {
            Attack();
        }
        else
        {
            cancelAttackTimer = false;
        }
    }
    public void AttackCall()
    {
        Debug.Log("attack!");
        switch (nextAttackType)
        {
            case AttackType.Meteor:
                MeteorAttack(player.transform.position);
            break;
            case AttackType.IceShard:
                IceAttack(player.transform.position);
            break;           
        }
        cancelAttackTimer = false;
        ChooseNextAttack();
    }
    void Attack()
    {
        AttackCall();
        StartCoroutine( AttackRoutine() );
    }

    public void MeteorAttack(Vector3 attackPoint)
    {
        Transform m = Instantiate(meteor).transform;
        m.GetComponent<Meteor>().Set(attackPoint);
    }
    public void IceAttack(Vector3 attackPoint)
    {
        Transform m = Instantiate(iceShard).transform;
        m.GetComponent<IceShard>().Set(attackPoint);
    }
    void ChooseNextAttack()
    {
        nextAttackType = attackTypes[Random.Range(0,attackTypes.Length)];
    }
}

public enum AttackType
{
    Meteor,IceShard
}
