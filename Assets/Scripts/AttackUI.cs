using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour
{
    public Image nextAttackImage;
    public Sprite meteor, ice, ligtning;
    public void SetNextAttackImage(AttackType a)
    {
        switch (a)
        {
            case AttackType.Meteor:
                nextAttackImage.sprite = meteor;
            break;
            case AttackType.IceShard:
                nextAttackImage.sprite = ice;
            break; 
            case AttackType.Lightning:
                nextAttackImage.sprite = ligtning;
            break;           
        }
    }
}
