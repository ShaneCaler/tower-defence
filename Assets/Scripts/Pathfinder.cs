using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField] Waypoint start, end;
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isRunning = true; // todo make private
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();


    Vector2Int[] directions =
    {
        Vector2Int.up, // (0,1)
        Vector2Int.right, // (1, 0)
        Vector2Int.down, // (0, -1)
        Vector2Int.left // (-1, 0)  
    };

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }

    private void CreatePath()
    {
        SetAsPath(end);
        Waypoint previous = end.exploredFrom;
        while (previous != start)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom; // move backwards thru list
        }
        SetAsPath(start);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(start);
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbors();
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == end)
        {
            isRunning = false;
        }
    }
    private void ExploreNeighbors()
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = searchCenter.GetGridPosition() + direction;
            if (grid.ContainsKey(neighborCoords))
            {
                QueueNewNeighbors(neighborCoords);
            }
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoords)
    {
        Waypoint neighbor = grid[neighborCoords];
        if (neighbor.isExplored || queue.Contains(neighbor))
        {
            // do nothing
        }
        else
        {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
        }

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
