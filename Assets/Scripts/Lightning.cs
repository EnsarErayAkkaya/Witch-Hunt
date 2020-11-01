using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public HeartCreator heartCreator;
    public GameObject attackPointImagePrefab;
    private GameObject attackPointImage;
    private Damager damager;
    public Vector3 attackPoint;
    public Vector3 attackPointOffset;
    public MeshFilter mesh;
    public float waitBeforeFallTime;
    public float cloudFallTime;
    public Collider collider;
    public float disappearDuration;
    public float startDisappearAfter;
    public Vector3 disappearPos;
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
        AudioManager.instance.Play("LigthningComing");
        this.attackPoint.y = 0;
        float t = 0;
        while (t < cloudFallTime)
        {
            t += Time.deltaTime * (Time.timeScale / cloudFallTime);
            transform.position = Vector3.Lerp(transform.position, attackPoint, t);
            yield return null;
        }
        heartCreator.CreateHeart(attackPoint + Vector3.forward);
        StartCoroutine( Disappear() );
    }
    private void OnTriggerEnter(Collider other) 
    {
        if( other.GetComponent<Health>() )
        {
            damager.GiveDamage(other.GetComponent<Health>());
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        if( other.GetComponent<Health>() )
        {
            damager.GiveDamage(other.GetComponent<Health>());
        }
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(startDisappearAfter);
        GameObject impactVFX = Instantiate(impactPrefab,mesh.transform.position,Quaternion.Euler(new Vector3(-90,0,0)));
        Destroy(impactVFX,3);
        Destroy(attackPointImage);
        Destroy(gameObject);
    }
}
