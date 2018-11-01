using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;
    Queue<Tower> towers = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        var towersInScene = towers.Count;

        if (towersInScene < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    } 

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.baseWaypoint = baseWaypoint;
        newTower.transform.parent = towerParentTransform.transform;
        baseWaypoint.isPlaceable = false;
        towers.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = towers.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;
        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;

        towers.Enqueue(oldTower);
    }
}
