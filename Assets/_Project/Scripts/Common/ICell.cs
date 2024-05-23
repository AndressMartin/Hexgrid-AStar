using Tiles;
using UnityEngine;

public interface ICell
{
    public Tile Tile { get; }
    public int Weight { get; }
    public Hex Hex { get; }
    public Vector3 CellPosition { get; }
}