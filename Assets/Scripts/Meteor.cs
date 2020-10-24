using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Damager damager;
    public Vector3 attackPoint;
    public Vector3 attackPointOffset;
    public Mesh crushedMeteor;
    public MeshFilter mesh;
    public float fallTime;
    public Collider collider;
    public float disappearDuration;
    public float startDisappearAfter;
    public Vector3 disappearPos;
    private void Start() {
        damager = GetComponent<Damager>();
    }
    public void Set(Vector3 attackPoint)
    {
        this.attackPoint = attackPoint;
        transform.position = attackPoint + attackPointOffset;
        mesh.gameObject.SetActive(true);
        StartCoroutine( Attack() );
    }
    IEnumerator Attack()
    {
        this.attackPoint.y = 0;
        float t = 0;
        while (t < fallTime)
        {
            t += Time.deltaTime / (Time.timeScale * fallTime);
            transform.position = Vector3.Lerp(transform.position, attackPoint, t);
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if( other.CompareTag("Ground") )
        {
            mesh.mesh = crushedMeteor;
            collider.enabled = false;
            StartCoroutine( Disappear() );
        }
        else if( other.GetComponent<Health>() )
        {
            damager.GiveDamage(other.GetComponent<Health>());
        }
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(startDisappearAfter);
        float t = 0;
        disappearPos = mesh.transform.position + disappearPos;
        while(t < disappearDuration)
        {
            t += Time.deltaTime / disappearDuration;
            mesh.transform.position = Vector3.Lerp(mesh.transform.position, disappearPos, t);
            yield return null;
        }
        Destroy(gameObject);
    }   
}
