using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCreator : MonoBehaviour
{
    public GameObject heart;
    public float heartAppearChance;
    public void CreateHeart(Vector3 point)
    {
        int i = Random.Range(0,100);
        if( i < heartAppearChance)
        {
            Instantiate(heart, point, Quaternion.identity);
        }
    }
}
