using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(menuName = "Grid/Tile")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private int weight;

        [Header("UI Settings")]
        [SerializeField] private Color tileColor;

        public Sprite Sprite => sprite;
        public int Weight => weight;
        public Color Color => tileColor;
    }
}
