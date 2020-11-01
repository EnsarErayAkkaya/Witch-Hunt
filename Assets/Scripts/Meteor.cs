using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public HeartCreator heartCreator;
    public GameObject attackPointImagePrefab;
    private GameObject attackPointImage;
    private Damager damager;
    public Vector3 attackPoint;
    public Vector3 attackPointOffset;
    public MeshFilter mesh;
    public float waitBeforeFallTime;
    public float meteorFallTime;
    public Collider collider;
    public float disappearDuration;
    public float startDisappearAfter;
    public Vector3 disappearPos;
    public GameObject smokeParticle;
    public GameObject impactPrefab;
    public void Set(Vector3 attackPoint)
    {
        damager = GetComponent<Damager>();
        heartCreator = FindObjectOfType<HeartCreator>();
        this.attackPoint = attackPoint;
        
        attackPointImage = Instantiate(attackPointImagePrefab);
        attackPointImage.transform.position = attackPoint + new Vector3(0, .1f, 0);

        transform.position = attackPoint + attackPointOffset;

        StartCoroutine( Attack() );
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(waitBeforeFallTime);
        this.attackPoint.y = -0.1f;
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

            AudioManager.instance.Play("meteorExplosion");

            Destroy(attackPointImage);
            Destroy(gameObject); 
            collider.enabled = false;
            StartCoroutine( Disappear() );
            heartCreator.CreateHeart(attackPoint);
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
