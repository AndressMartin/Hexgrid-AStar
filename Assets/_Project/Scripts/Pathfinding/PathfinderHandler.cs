using System.Collections.Generic;
using UnityEngine;

public class PathfinderHandler : MonoBehaviour
{
    [SerializeField] private Node startNode;
    [SerializeField] private Node endNode;
    [SerializeField] private LineRenderer lineRenderer;

    private IList<ICell> path;

    private void Start()
    {
        Dictionary<Hex, ICell> nodes = GetNodesDictionary();

        HexPathfinder pathfinder = new();
        path = pathfinder.FindPathOnMap(startNode, endNode, new HexMap(nodes));

        if (lineRenderer != null && path.Count > 1)
        {
            DrawPath();
        }
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

    private void DrawPath()
    {
        lineRenderer.positionCount = path.Count;

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 position = path[i].CellPosition;
            lineRenderer.SetPosition(i, position);
        }
    }

    private void OnDrawGizmos()
    {
        if (startNode != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(startNode.transform.position, 0.1f);
        }

        if (endNode != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(endNode.transform.position, 0.1f);
        }
    }
}