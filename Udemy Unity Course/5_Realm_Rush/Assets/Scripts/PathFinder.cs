using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
                                Vector2Int.up,
                                Vector2Int.right,
                                Vector2Int.down,
                                Vector2Int.left};


    public List<Waypoint> GetPath()
    {
        if(path.Count == 0)
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
        path.Add(endWaypoint);
        endWaypoint.isPlaceable = false;
        
        Waypoint previous= endWaypoint.exploredFrom;

        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous.isPlaceable = false;
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        startWaypoint.isPlaceable = false;
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count>0&& isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbors();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors()
    {
        if (!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = searchCenter.GetGridPos()+ direction;
            if(grid.ContainsKey(neighborCoordinates))
            {
                QueueNewNeighbors(neighborCoordinates);
            }  
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoordinates)
    {
        Waypoint neighbor = grid[neighborCoordinates];
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
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping Block: " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

}
