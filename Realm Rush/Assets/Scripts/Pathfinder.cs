using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWayPoint, endWayPoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint topWaypoint;
    private List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
        
    }

    private void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWayPoint);
        

        Waypoint prev = endWayPoint.exploredFrom;
        while (prev != startWayPoint)
        {
            SetAsPath(prev);
            prev = prev.exploredFrom;
        }
        //add start waypoint and reverse list to get full path list
        SetAsPath(startWayPoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWayPoint);
        while (queue.Count > 0 && isRunning)
        {
            topWaypoint = queue.Dequeue();
            topWaypoint.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbors();
            
        }
    }

    private void HaltIfEndFound()
    {
        if (topWaypoint == endWayPoint)
        {
            isRunning = false;
            return;
        }
    }

    private void ExploreNeighbors()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = topWaypoint.getGridPos() + direction;
            if (grid.ContainsKey(neighborCoords))
            {
                QueueNewNeighbors(neighborCoords);
            }
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoords)
    {
        Waypoint neighbor = grid[neighborCoords];
        if (!neighbor.isExplored && !queue.Contains(neighbor))
        {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = topWaypoint;
            //print("queueing: " + neighbor.name);
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            //overlapping blocks arent allowed
            //add to dict if not overlapping
            if (grid.ContainsKey(waypoint.getGridPos()))
            {
                //Debug.Log("Skipping overlapping block" + waypoint);
            }
            else
            {
                grid.Add(waypoint.getGridPos(), waypoint);
            }
        }
        //print("loaded" + grid.Count + " blocks");
    }

}
