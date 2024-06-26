using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Hex
{
    public static float RADIUS = 0.5f;
    public static Vector2 Q_BASIS = new Vector2(2f, 0) * RADIUS;
    public static Vector2 R_BASIS = new Vector2(1f, Mathf.Sqrt(3)) * RADIUS;
    public static Vector2 Q_INV = new Vector2(1f / 2, -Mathf.Sqrt(3) / 6);
    public static Vector2 R_INV = new Vector2(0, Mathf.Sqrt(3) / 3);

    //Axial coordinates
    public int q;
    public int r;
    
    public static Hex[] AXIAL_DIRECTIONS = {
        new(1, 0), // 0
        new(0, 1), // 1
        new(-1, 1), // 2
        new(-1, 0), // 3
        new(0, -1), // 4
        new(1, -1), // 5
    };

    public static Hex FromPlanar(Vector2 planar)
    {
        float q = Vector2.Dot(planar, Q_INV) / RADIUS;
        float r = Vector2.Dot(planar, R_INV) / RADIUS;
        return new Hex(q, r);
    }

    public static Hex FromWorld(Vector3 world)
    {
        return FromPlanar(world.WorldToPlanar());
    }

    public static Hex operator +(Hex a, Hex b)
    {
        return new Hex(a.q + b.q, a.r + b.r);
    }

    public static Hex operator -(Hex a, Hex b)
    {
        return new Hex(a.q - b.q, a.r - b.r);
    }

    public static Hex zero = new Hex(0, 0);
    
    public Hex(float q, float r) : this(Mathf.RoundToInt(q), Mathf.RoundToInt(r))
    {
    }

    public Hex(int q, int r)
    {
        this.q = q;
        this.r = r;
    }

    public static IEnumerable<Hex> Ring(Hex center, int radius)
    {
        Hex current = center + new Hex(0, -radius);
        foreach (Hex dir in AXIAL_DIRECTIONS)
        {
            for (int i = 0; i < radius; i++)
            {
                yield return current;
                current = current + dir;
            }
        }
    }

    public static IEnumerable<Hex> Spiral(Hex center, int minRadius, int maxRadius)
    {
        if (minRadius == 0)
        {
            yield return center;
            minRadius += 1;
        }

        for (int r = minRadius; r <= maxRadius; r++)
        {
            var ring = Ring(center, r);
            foreach (Hex hex in ring)
            {
                yield return hex;
            }
        }
    }

    public static IEnumerable<Hex> FloodFill(IEnumerable<Hex> startFrom)
    {
        HashSet<Hex> visited = new HashSet<Hex>();
        Queue<Hex> frontier = new Queue<Hex>(startFrom);
        while (frontier.Count > 0)
        {
            Hex current = frontier.Dequeue();
            yield return current;
            foreach (Hex next in current.Neighbours())
            {
                if (visited.Contains(next))
                {
                    continue;
                }

                visited.Add(next);
                frontier.Enqueue(next);
            }
        }
    }

    public Vector2 ToPlanar()
    {
        return Q_BASIS * q + R_BASIS * r;
    }

    public Vector3 ToWorld(float z = 0f)
    {
        return ToPlanar().PlanarToWorld(z);
    }

    public IEnumerable<Hex> Neighbours()
    {
        foreach (Hex dir in AXIAL_DIRECTIONS)
        {
            yield return this + dir;
        }
    }

    public Hex GetNeighbour(int dir)
    {
        Hex incr = AXIAL_DIRECTIONS[dir % AXIAL_DIRECTIONS.Length];
        return this + incr;
    }

    public int DistanceTo(Hex to)
    {
        return (Mathf.Abs(q - to.q)
                + Mathf.Abs(q + r - to.q - to.r)
                + Mathf.Abs(r - to.r)) / 2;
    }

    public override bool Equals(System.Object obj)
    {
        Hex hex = (Hex)obj;
        return (q == hex.q) && (r == hex.r);
    }

    public override int GetHashCode()
    {
        return 23 + 31 * q + 37 * r;
    }

    public override string ToString()
    {
        return "(" + q + ";" + r + ")";
    }
}