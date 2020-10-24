using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject attackPointImagePrefab;
    private GameObject attackPointImage;
    private Damager damager;
    public Vector3 attackPoint;
    public Vector3 attackPointOffset;
    public Mesh crushedMeteor;
    public MeshFilter mesh;
    public float waitBeforeFallTime;
    public float meteorFallTime;
    public Collider collider;
    public float disappearDuration;
    public float startDisappearAfter;
    public Vector3 disappearPos;
    public GameObject smokeParticle;
    public GameObject impactPrefab;

    private void Start() {
        damager = GetComponent<Damager>();
    }
    public void Set(Vector3 attackPoint)
    {
        this.attackPoint = attackPoint;
        
        attackPointImage = Instantiate(attackPointImagePrefab);
        attackPointImage.transform.position = attackPoint + new Vector3(0, .1f, 0);

        transform.position = attackPoint + attackPointOffset;

        mesh.gameObject.SetActive(true);
        StartCoroutine( Attack() );
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(waitBeforeFallTime);
        this.attackPoint.y = 0;
        float t = 0;
        while (t < meteorFallTime)
        {
            t += Time.deltaTime * (Time.timeScale / meteorFallTime);
            transform.position = Vector3.Lerp(transform.position, attackPoint, t);
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if( other.CompareTag("Ground") )
        {
            GameObject impactVFX = Instantiate(impactPrefab,transform.position,Quaternion.identity);
            Destroy(impactVFX,3);
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
        Destroy(smokeParticle);
        float t = 0;
        disappearPos = mesh.transform.position + disappearPos;
        while(t < disappearDuration)
        {
            t += Time.deltaTime / disappearDuration;
            mesh.transform.position = Vector3.Lerp(mesh.transform.position, disappearPos, t);
            yield return null;
        }
        Destroy(attackPointImage);
        Destroy(gameObject);
    }   
}
