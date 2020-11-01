using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject impactPrefab;
    public float healthAmount, duration;
    public Vector3 rotateVector;
    private void Start() 
    {
        Destroy(gameObject,duration);
    }
    private void Update() {
        transform.Rotate(rotateVector * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<Health>().GainHealth(healthAmount);
        }
    }
    private void OnDestroy() {
        AudioManager.instance.Play("heartDestroy");
        GameObject impactVFX = Instantiate(impactPrefab,transform.position,Quaternion.identity);
        Destroy(impactVFX,3);
    }
}
