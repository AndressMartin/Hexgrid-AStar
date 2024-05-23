using System.Collections.Generic;
using UnityEngine;

public class HexPathfinder : IPathFinder
{
    public IList<ICell> FindPathOnMap(ICell cellStart, ICell cellEnd, IMap map)
    {
        Dictionary<Hex, ICell> cells = map.Cells;

        if (!cells.ContainsKey(cellStart.Hex) || !cells.ContainsKey(cellEnd.Hex))
        {
            Debug.LogWarning("Start or End hex is not in the cell dictionary.");
            return new List<ICell>();
        }

        // Initialize open and closed sets
        HashSet<Hex> closedSet = new HashSet<Hex>();
        PriorityQueue<Hex> openSet = new PriorityQueue<Hex>();
        openSet.Enqueue(cellStart.Hex, 0);

        // Maps for costs and paths
        Dictionary<Hex, Hex> cameFrom = new Dictionary<Hex, Hex>();
        Dictionary<Hex, float> gScore = new Dictionary<Hex, float> { { cellStart.Hex, 0 } };
        Dictionary<Hex, float> fScore = new Dictionary<Hex, float> { { cellStart.Hex, HeuristicCostEstimate(cellStart.Hex, cellEnd.Hex) } };

        // Loop until the open set is empty or the end node is reached
        while (openSet.Count > 0)
        {
            Hex current = openSet.Dequeue();
            if (current.Equals(cellEnd.Hex))
                return ReconstructPath(cameFrom, current, cells);

            closedSet.Add(current);
            foreach (Hex neighbor in current.Neighbours())
            {
                if (!cells.ContainsKey(neighbor) || closedSet.Contains(neighbor) || cells[neighbor].Weight < 0)
                    continue;

                float tentative_gScore = gScore[current] + cells[neighbor].Weight; // Consider cell weight
                if (!gScore.ContainsKey(neighbor))
                    gScore[neighbor] = float.MaxValue;

                if (tentative_gScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative_gScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, cellEnd.Hex);
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }

        return new List<ICell>(); // Return an empty list if a path is not found
    }

    private float HeuristicCostEstimate(Hex a, Hex b)
    {
        return a.DistanceTo(b);
    }

    private IList<ICell> ReconstructPath(Dictionary<Hex, Hex> cameFrom, Hex current, Dictionary<Hex, ICell> cells)
    {
        List<ICell> totalPath = new List<ICell> { cells[current] };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(cells[current]);
        }
        totalPath.Reverse();
        return totalPath;
    }
}