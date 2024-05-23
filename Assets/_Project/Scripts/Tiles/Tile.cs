using System;
using UnityEngine;

namespace Tiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileData data;
        [SerializeField] private SpriteRenderer tileSprite;
        [SerializeField] private SpriteRenderer iconSprite;
        
        public TileData Data => data;
        public SpriteRenderer IconSprite => iconSprite;

        // Assign the correct sprite related to the data asset
        private void OnValidate()
        {
            if (!data || !tileSprite)
                return;
            
            tileSprite.sprite = data.Sprite;
        }
    }
}