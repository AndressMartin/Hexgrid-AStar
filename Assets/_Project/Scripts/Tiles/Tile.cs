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
        private Node node;

        public TileData Data => data;
        public SpriteRenderer IconSprite => iconSprite;
        public Node Node => node;
        
        public void Initialize(Node node)
        {
            this.node = node;
        }

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
            switch(ClickActionHandler.ActionType)
            {
                case TileClickActionType.Paint:
                    TileData dataToPaint = TilePainter.OnTilePainted?.Invoke();
                    if (dataToPaint)
                    {
                        data = dataToPaint;
                        PaintTile();
                    }
                    break;
                case TileClickActionType.SetStart:
                    PathfinderHandler.OnStartNodeSet?.Invoke(Node);
                    break;
                case TileClickActionType.SetEnd:
                    PathfinderHandler.OnEndNodeSet?.Invoke(Node);
                    break;
            }
            
        }
    }
}