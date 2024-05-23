using Tiles;
using UnityEngine;

public class TileUIOptions : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private TileEntry tileEntryPrefab;
    [SerializeField] private TileGroup tileGroup;
    
    [Header("UI Scene Objects")]
    [SerializeField] private RectTransform tileOptionsParent;
    
    private void Start()
    {
        foreach (TileData tile in tileGroup.Tiles)
        {
            TileEntry tileEntry = Instantiate(tileEntryPrefab, tileOptionsParent);
            tileEntry.Initialize(tile);
        }
    }
}