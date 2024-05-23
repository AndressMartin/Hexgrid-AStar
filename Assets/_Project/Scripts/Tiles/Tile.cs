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
            PaintTile();
        }

        private void PaintTile()
        {
            if (!data || !tileSprite)
                return;

            tileSprite.sprite = data.Sprite;
        }

        //Get when a player clicks on the SpriteRenderer
        private void OnMouseDown()
        {
            Debug.LogWarning($"Tile {data.name} was clicked.");
            TileData dataToPaint = TilePainter.OnTilePainted?.Invoke();
            if (dataToPaint)
            {
                data = dataToPaint;
                PaintTile();
            }
        }
    }
}