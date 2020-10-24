using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 attackPoint;
    public Vector3 attackPointOffset;
    public Mesh crushedMeteor;
    public MeshFilter mesh;
    public float fallTime;
    public void Set(Vector3 attackPoint)
    {
        this.attackPoint = attackPoint;
        transform.position = attackPoint + attackPointOffset;
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine( Attack() );
    }
    IEnumerator Attack()
    {
        float t = 0;
        while (t < fallTime)
        {
            t += Time.deltaTime / fallTime;
            transform.position = Vector3.Lerp(transform.position, attackPoint, t);
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if( other.CompareTag("Ground") )
        {
            mesh.mesh = crushedMeteor;
        }
    }
}
