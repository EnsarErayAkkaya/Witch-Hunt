using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseZombie : MonoBehaviour
{
    public AIDestinationSetter aiDestinationSetter;
    public AIPath aIPath;
    public bool stopped;
    private void Start() 
    {
        aiDestinationSetter.target = FindObjectOfType<Player>().transform;
    }
    public void StopZombie()
    {
        stopped = true;
        aIPath.isStopped = true;
    }
    public void ContinueZombie()
    {
        stopped = false;
        aIPath.isStopped = false;
    }
}
