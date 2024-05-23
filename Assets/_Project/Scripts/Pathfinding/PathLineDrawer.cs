using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lerpDuration = 0.2f;

    public void DrawPath(IList<ICell> path)
    {
        lineRenderer.positionCount = 0;
        StartCoroutine(LerpPath(path));
    }

    private IEnumerator LerpPath(IList<ICell> path)
    {
        lineRenderer.positionCount = 0;

        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 startPosition = path[i].CellPosition;
            Vector3 endPosition = path[i + 1].CellPosition;

            float elapsedTime = 0f;
            while (elapsedTime < lerpDuration)
            {
                elapsedTime += Time.deltaTime;
                Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / lerpDuration);

                if (lineRenderer.positionCount < i + 2)
                {
                    lineRenderer.positionCount = i + 2;
                }

                lineRenderer.SetPosition(i, startPosition);
                lineRenderer.SetPosition(i + 1, currentPosition);

                yield return null;
            }

            lineRenderer.SetPosition(i + 1, endPosition);
        }
        PathfinderHandler.OnPathFinishedDrawing?.Invoke();
    }
}