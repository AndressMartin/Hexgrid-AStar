using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderHandler : MonoBehaviour
{
    public static Action OnNewPathRequested;
    public static Action OnPathFinishedDrawing;
    public static Action<Node> OnStartNodeSet;
    public static Action<Node> OnEndNodeSet;
    
    [Header("Pathfinding")]
    [SerializeField] private Node startNode;
    [SerializeField] private Node endNode;
    [SerializeField] private PathLineDrawer pathDrawer;

    [Header("Visuals")] 
    [SerializeField] private Sprite StartSprite;
    [SerializeField] private Sprite EndSprite;
    
    private IList<ICell> path;

    private void Start()
    {
        startNode.Tile.IconSprite.sprite = StartSprite;
        endNode.Tile.IconSprite.sprite = EndSprite;
        Dictionary<Hex, ICell> nodes = GetNodesDictionary();

        HexPathfinder pathfinder = new();
        path = pathfinder.FindPathOnMap(startNode, endNode, new HexMap(nodes));
        if (path.Count > 0)
        {
            HexPathfinder.OnPathSuccess?.Invoke();
            if (pathDrawer != null && path.Count > 1)
            {
                pathDrawer.DrawPath(path);
            }
        }
        else
        {
            pathDrawer.ClearPath();
            HexPathfinder.OnPathFailed?.Invoke();
        }
        
        OnNewPathRequested += GenerateNewPath;
        OnStartNodeSet += SetStartNode;
        OnEndNodeSet += SetEndNode;
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

        if (path.Count > 0)
        {
            HexPathfinder.OnPathSuccess?.Invoke();
            if (pathDrawer != null && path.Count > 1)
            {
                pathDrawer.DrawPath(path);
            }
        }
        else
        {
            pathDrawer.ClearPath();
            HexPathfinder.OnPathFailed?.Invoke();
        }
    }
    
    private void SetStartNode(Node node)
    {
        startNode.Tile.IconSprite.sprite = null;
        startNode = node;
        startNode.Tile.IconSprite.sprite = StartSprite;
        pathDrawer.ClearPath();
    }
    
    private void SetEndNode(Node node)
    {
        endNode.Tile.IconSprite.sprite = null;
        endNode = node;
        endNode.Tile.IconSprite.sprite = EndSprite;
        pathDrawer.ClearPath();
    }
}