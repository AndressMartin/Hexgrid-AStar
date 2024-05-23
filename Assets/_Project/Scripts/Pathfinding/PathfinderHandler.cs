using System.Collections.Generic;
using UnityEngine;

public class PathfinderHandler : MonoBehaviour
{
    [Header("Node Sprites")]
    [SerializeField] private Sprite startNodeIconSprite;
    [SerializeField] private Sprite endNodeIconSprite;
    
    [Header("Path Setup")]
    [SerializeField] private Node startNode;
    [SerializeField] private Node endNode;
    [SerializeField] private LineRenderer lineRenderer;

    private List<Hex> path;

    private void Start()
    {
        if (!startNode || !endNode)
        {
            Debug.LogWarning("Start or End node is not assigned.");
            return;
        }
        
        Dictionary<Hex, Node> nodes = GetNodesDictionary();
        
        startNode.Tile.IconSprite.sprite = startNodeIconSprite;
        endNode.Tile.IconSprite.sprite = endNodeIconSprite;
        
        HexPathfinder pathfinder = new();
        path = pathfinder.FindPath(startNode.Hex, endNode.Hex, nodes);

        if (lineRenderer != null && path.Count > 1)
        {
            DrawPath();
        }
    }

    private Dictionary<Hex, Node> GetNodesDictionary()
    {
        Dictionary<Hex, Node> nodes = new Dictionary<Hex, Node>();

        foreach (Node node in FindObjectsOfType<Node>())
        {
            nodes[node.Hex] = node;
        }

        return nodes;
    }

    private void DrawPath()
    {
        lineRenderer.positionCount = path.Count;

        for (int i = 0; i < path.Count; i++)
        {
            Hex hex = path[i];
            Vector3 position = hex.ToWorld();
            lineRenderer.SetPosition(i, position);
        }
    }
}