using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAttack : MonoBehaviour
{
    public AttackUI attackUI;
    public Player player;
    public float normalAttackSpeed, noFigthingAttackSpeed;
    float currentAttackSpeed;
    public AttackType[] attackTypes;
    public AttackType nextAttackType;
    public bool willAttack, playerInFight;
    public GameObject meteor,iceShard,ligthning;

    private void Start() 
    {
        attackUI = FindObjectOfType<AttackUI>();
        player = FindObjectOfType<Player>();
        ChangeAttackSpeed(false );
        ChooseNextAttack();
        StartCoroutine( AttackRoutine() );
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds( currentAttackSpeed );
        Attack();
    }
    public void AttackCall()
    {
        if(willAttack)
        {
            switch (nextAttackType)
            {
                case AttackType.Meteor:
                    MeteorAttack(player.transform.position);
                break;
                case AttackType.IceShard:
                    IceAttack(player.transform.position);
                break; 
                case AttackType.Lightning:
                    LightiningAttack(player.transform.position);
                break;           
            }
        }
       
        ChooseNextAttack();
    }
    void Attack()
    {
        AttackCall();
        StartCoroutine( AttackRoutine() );
    }
    public void ChangeAttackSpeed(bool thereIsEnemy)
    {
        if(thereIsEnemy)
        {
            currentAttackSpeed = normalAttackSpeed;
            playerInFight = true;
        }
        else
        {
            currentAttackSpeed = noFigthingAttackSpeed;
            playerInFight = false;
        }
    }

    public void MeteorAttack(Vector3 attackPoint)
    {
        Transform m = Instantiate(meteor).transform;
        m.GetComponent<Meteor>().Set(attackPoint);
    }
    public void LightiningAttack(Vector3 attackPoint)
    {
        Transform m = Instantiate(ligthning).transform;
        m.GetComponent<Lightning>().Set(attackPoint);
    }
    public void IceAttack(Vector3 attackPoint)
    {
        Transform m = Instantiate(iceShard).transform;
        m.GetComponent<IceShard>().Set(attackPoint);
    }
    void ChooseNextAttack()
    {
        nextAttackType = attackTypes[Random.Range(0,attackTypes.Length)];
        attackUI.SetNextAttackImage(nextAttackType);
    }
}

public enum AttackType
{
    Meteor,IceShard,Lightning
}
