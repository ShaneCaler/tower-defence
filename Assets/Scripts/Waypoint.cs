using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    [SerializeField] Color defaultColor;
    [SerializeField] Color exploredColor;

    Vector2Int gridPos;
    const int gridSize = 10;
    public bool isExplored = false; // OK to be public b/c this is a data class
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

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
	// Update is called once per frame
	void Update () {
        if (waypoint.isExplored)
        {
            waypoint.SetTopColor(exploredColor);
        }
        else
        {
            waypoint.SetTopColor(defaultColor);
        }

    }
}
