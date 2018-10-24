using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] Waypoint start, end;
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isRunning = true; // todo make private

    Vector2Int[] directions =
    {
        Vector2Int.up, // (0,1)
        Vector2Int.right, // (1, 0)
        Vector2Int.down, // (0, -1)
        Vector2Int.left // (-1, 0)  
    };
    void Start ()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();

        //ExploreNeighbors();
    }

    private void Pathfind()
    {
        queue.Enqueue(start);
        while(queue.Count > 0)
        {
            var searchCenter = queue.Dequeue();
            print("Searching from: " + searchCenter);
            HaltIfEndFound(searchCenter);
        }
        print("finished pathfinding");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == end)
        {
            print("Start is equal to end, stopping Pathfind");
            isRunning = false;
        }
    }
    private void ExploreNeighbors()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoords = start.GetGridPosition() + direction;
            if (grid.ContainsKey(explorationCoords))
            {
                grid[explorationCoords].SetTopColor(Color.yellow);
            }
        }
    }

    private void ColorStartAndEnd()
    {
        start.SetTopColor(Color.blue);
        end.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPosition();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skpping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    void Update () {
		
	}
}
