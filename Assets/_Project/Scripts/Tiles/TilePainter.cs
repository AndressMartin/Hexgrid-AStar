using System;
using Tiles;
using UnityEngine;

public class TilePainter : MonoBehaviour
{
    public static Func<TileData> OnTilePainted;
    
    [SerializeField] private TilePicker tilePicker;
    
    private void Start()
    {
        OnTilePainted += PaintTile;
    }

    private TileData PaintTile()
    {
        return tilePicker.ChosenTile;
    }
}