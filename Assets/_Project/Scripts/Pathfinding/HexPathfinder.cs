using System.Collections.Generic;
using UnityEngine;

public class HexPathfinder
{
    public List<Hex> FindPath(Hex startHex, Hex endHex, Dictionary<Hex, Node> nodes)
    {
        if (!nodes.ContainsKey(startHex) || !nodes.ContainsKey(endHex))
        {
            Debug.LogWarning("Start or End hex is not in the node dictionary.");
            return new List<Hex>();
        }

        // Initialize open and closed sets
        HashSet<Hex> closedSet = new HashSet<Hex>();
        PriorityQueue<Hex> openSet = new PriorityQueue<Hex>();
        openSet.Enqueue(startHex, 0);

        // Maps for costs and paths
        Dictionary<Hex, Hex> cameFrom = new Dictionary<Hex, Hex>();
        Dictionary<Hex, float> gScore = new Dictionary<Hex, float> { { startHex, 0 } };
        Dictionary<Hex, float> fScore = new Dictionary<Hex, float> { { startHex, HeuristicCostEstimate(startHex, endHex) } };

        // Loop until the open set is empty or the end node is reached
        while (openSet.Count > 0)
        {
            Hex current = openSet.Dequeue();
            if (current.Equals(endHex))
                return ReconstructPath(cameFrom, current);

            closedSet.Add(current);
            foreach (Hex neighbor in current.Neighbours())
            {
                if (!nodes.ContainsKey(neighbor) || closedSet.Contains(neighbor) || nodes[neighbor].Weight < 0)
                    continue;

                float tentative_gScore = gScore[current] + nodes[neighbor].Weight;
                if (!gScore.ContainsKey(neighbor))
                    gScore[neighbor] = float.MaxValue;

                if (tentative_gScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative_gScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, endHex);
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }

        return new List<Hex>(); // Return an empty list if a path is not found
    }

    private float HeuristicCostEstimate(Hex a, Hex b)
    {
        return a.DistanceTo(b);
    }

    private List<Hex> ReconstructPath(Dictionary<Hex, Hex> cameFrom, Hex current)
    {
        List<Hex> totalPath = new List<Hex> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(current);
        }
        totalPath.Reverse();
        return totalPath;
    }
}