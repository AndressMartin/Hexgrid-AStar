using System.Collections.Generic;
using UnityEngine;

public class PathLineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void DrawPath(IList<ICell> path)
    {
        lineRenderer.positionCount = path.Count;

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 position = path[i].CellPosition;
            lineRenderer.SetPosition(i, position);
        }
    }
}