using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public StoryScript storyScript;
    public WitchAttack attacker;
    public Collider collider;
    public GameObject ranged,melee;
    public Vector3[] meleeEnemyStartPoses,rangedEnemyStartPoses;
    public float enemyExtraDeph;
    public List<Transform> liveEnemies = new List<Transform>();
    public int storyIndex;
    private void Start() 
    {
        storyScript = FindObjectOfType<StoryScript>();
        attacker = FindObjectOfType<WitchAttack>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            collider.enabled = false;
            attacker.ChangeAttackSpeed(true);
            SpawnEnemies();
        }
    }
    public void SpawnEnemies()
    {
        
        foreach (var item in meleeEnemyStartPoses)
        {
            Vector3 v = new Vector3(item.x, item.y-enemyExtraDeph, item.z );
            liveEnemies.Add( Instantiate(melee, transform.position + v, Quaternion.identity).transform );
            liveEnemies[liveEnemies.Count-1].GetComponent<Zombie>().Set(this);
        }
        foreach (var item in rangedEnemyStartPoses)
        {
            Vector3 v = new Vector3(item.x, item.y-enemyExtraDeph, item.z );
            liveEnemies.Add( Instantiate(ranged, transform.position + v, Quaternion.identity).transform );
            liveEnemies[liveEnemies.Count-1].GetComponent<RangedZombie>().Set(this);
        }
        StartCoroutine( MoveEnemiesUp() );
    }
    IEnumerator MoveEnemiesUp()
    {
        if(storyIndex != -1)
        {
            foreach (var item in liveEnemies)
            {
                item.GetComponent<BaseZombie>().StopZombie();
            }
            storyScript.SetEnemySpawner(this);
            storyScript.ShowRelatedStory(storyIndex);
        }
        float t = 0;
        Vector3 v = new Vector3(0, -enemyExtraDeph, 0);
        while (t < 2.0f)
        {
            t += Time.deltaTime;
            v = Vector3.Lerp(v, Vector3.zero, t);
            foreach (var item in liveEnemies)
            {
                item.transform.position = new Vector3(item.transform.position.x, v.y, item.transform.position.z);
            }
            yield return null;
        }
    }
    public void RemoveEnemy(Transform t)
    {
        liveEnemies.Remove(t);
        if(liveEnemies.Count == 0)
            attacker.ChangeAttackSpeed(false);
    }
    private void OnDrawGizmos() {
        foreach (var item in meleeEnemyStartPoses)
        {
            Gizmos.DrawCube(transform.position + item - new Vector3(0, enemyExtraDeph,0), new Vector3(1, 1, 1));
        }
        foreach (var item in rangedEnemyStartPoses)
        {
            Gizmos.DrawCube(transform.position + item - new Vector3(0, enemyExtraDeph,0), new Vector3(1, 1, 1));
        }
    }
}
