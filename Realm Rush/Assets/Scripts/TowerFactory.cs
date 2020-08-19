using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{


    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower tower;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> towerQueue = new Queue<Tower>();

    

    public void AddTower(Waypoint baseWaypoint)
    {
        int towerCount = towerQueue.Count;
        if (towerCount < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
        Debug.Log( "queue: " + towerQueue.Count);
    }
    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(tower, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform.transform;

        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;

        newBaseWaypoint.isPlaceable = false;
        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;
        towerQueue.Enqueue(oldTower);

    }

}
