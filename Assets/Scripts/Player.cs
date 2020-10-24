using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public WitchAttack witchAttack;
    public float mockCooldown;
    public float mockedAttackInterval;
    public bool canMock;
    private void Start() {
        canMock = true;
        witchAttack = FindObjectOfType<WitchAttack>();
    }
    private void Update() {
        Mock();
    }

    public void Mock()
    {
        if( Input.GetKeyDown(KeyCode.E) && canMock )
        {
            canMock = false;
            StartCoroutine( StartMock() );
            
            Debug.Log("HAHAHAHAHAA!");
            StartCoroutine( MockCooldownCounter() );
        }
    }
    IEnumerator StartMock()
    {
        witchAttack.AttackCall();
        yield return new WaitForSeconds(mockedAttackInterval);
        witchAttack.AttackCall();
        yield return new WaitForSeconds(mockedAttackInterval);
        witchAttack.AttackCall();
        
    }
    IEnumerator MockCooldownCounter()
    {
        yield return new WaitForSeconds(mockCooldown);
        canMock = true;
    }
}
