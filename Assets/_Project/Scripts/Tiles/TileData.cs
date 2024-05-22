using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(menuName = "World/Tile")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private int weight;

        public Sprite Sprite => sprite;
        public int Weight => weight;
    }
}
