using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(menuName = "Grid/TileGroup")]
    public class TileGroup: ScriptableObject
    {
        [SerializeField] private TileData[] tiles;

        public TileData[] Tiles => tiles;
    }
}