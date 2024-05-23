using System;
using Tiles;
using UnityEngine;

public class TilePicker : MonoBehaviour
{
    public static Action<TileData> OnTileChosen;
    public static Action OnTileCleared;
    
    private TileData chosenTile;
    public TileData ChosenTile => chosenTile;

    private void Start()
    {
        OnTileChosen += SetChosenTile;
        OnTileCleared += ClearChosenTile;
    }

    private void SetChosenTile(TileData tile)
    {
        chosenTile = tile;
    }
    
    private void ClearChosenTile()
    {
        chosenTile = null;
    }
}