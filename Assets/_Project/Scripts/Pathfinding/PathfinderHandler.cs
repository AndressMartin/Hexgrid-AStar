using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderHandler : MonoBehaviour
{
    public static Action OnNewPathRequested;
    public static Action OnPathFinishedDrawing;
    
    [SerializeField] private Node startNode;
    [SerializeField] private Node endNode;
    [SerializeField] private PathLineDrawer pathDrawer;

    private IList<ICell> path;

    private void Start()
    {
        Dictionary<Hex, ICell> nodes = GetNodesDictionary();

        HexPathfinder pathfinder = new();
        path = pathfinder.FindPathOnMap(startNode, endNode, new HexMap(nodes));

        if (pathDrawer != null && path.Count > 1)
        {
            pathDrawer.DrawPath(path);
        }
        
        OnNewPathRequested += GenerateNewPath;
    }

    private Dictionary<Hex, ICell> GetNodesDictionary()
    {
        Dictionary<Hex, ICell> nodes = new Dictionary<Hex, ICell>();

        foreach (Node node in FindObjectsOfType<Node>())
        {
            nodes[node.Hex] = node;
        }

        return nodes;
    }
    
    private void GenerateNewPath()
    {
        path = new HexPathfinder().FindPathOnMap(startNode, endNode, new HexMap(GetNodesDictionary()));

        if (pathDrawer != null && path.Count > 1)
        {
            pathDrawer.DrawPath(path);
        }
    }
}