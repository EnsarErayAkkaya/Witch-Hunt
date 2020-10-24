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
    public GameObject meteor;

    private void Start() 
    {
        currentAttackSpeed = normalAttackSpeed;
        StartCoroutine( Attacker() );
    }

    IEnumerator Attacker()
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

    public void Attack()
    {
        Debug.Log("attack!");
        switch (nextAttackType)
        {
            case AttackType.Meteor:
                MeteorAttack(player.transform.position);
            break;            
        }
        cancelAttackTimer = false;
        StartCoroutine( Attacker() );
    }

    public void MeteorAttack(Vector3 attackPoint)
    {
        Transform m = Instantiate(meteor).transform;
        m.GetComponent<Meteor>().Set(attackPoint);
    }
}

public enum AttackType
{
    Meteor
}
