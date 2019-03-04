using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;
    int numTowers = 0;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        numTowers = towerQueue.Count;
        if (numTowers < towerLimit)
        {
            InstatiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }

    }


    private void InstatiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        baseWaypoint.isPlaceable = false;
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
