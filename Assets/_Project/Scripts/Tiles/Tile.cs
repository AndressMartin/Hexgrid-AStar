using System;
using UnityEngine;

namespace Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileData data;
        
        public TileData Data => data;

        private void OnValidate()
        {
            GetComponent<SpriteRenderer>().sprite = data.Sprite;
        }
    }
}