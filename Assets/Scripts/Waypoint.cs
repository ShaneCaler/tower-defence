using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Waypoint : MonoBehaviour {

    Vector2Int gridPos;
    const int gridSize = 10;
    public bool isExplored = false; // OK to be public b/c this is a data class
    public bool isPlaceable = true;
	public bool hasTower;
    public Waypoint exploredFrom;
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && waypoint.isPlaceable)
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }
}
