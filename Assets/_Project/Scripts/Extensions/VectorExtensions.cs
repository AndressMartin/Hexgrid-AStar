using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 WorldToPlanar(this Vector3 world)
    {
        return new Vector2(world.x, world.y);
    }

    public static Vector3 PlanarToWorld(this Vector2 planar, float z = 0f)
    {
        return new Vector3(planar.x, planar.y, z);
    }

    public static Hex ToHex(this Vector3 world)
    {
        return Hex.FromWorld(world);
    }

    public static Hex ToHex(this Vector2 planar)
    {
        return Hex.FromPlanar(planar);
    }
}