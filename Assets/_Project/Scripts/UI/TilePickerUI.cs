using System;
using Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilePickerUI : MonoBehaviour
{
    [SerializeField] private Button clearChoiceButton;
    [SerializeField] private TextMeshProUGUI chosenTileText;
    [SerializeField] private TextMeshProUGUI tileInfoText;
    
    [Header("Text Colors")]
    [SerializeField] private Color noTileSelectedColor;
    [SerializeField] private Color tileSelectedColor;
    public static Action<Vector3, TileData> OnTileChosen;

    private void Start()
    {
        SetNoTileText();
        clearChoiceButton.onClick.AddListener(ClearChosenTile);
        TilePicker.OnTileChosen += SetChosenTileText;
        ClickActionHandler.ChangedActionType += actionType =>
        {
            if (actionType != TileClickActionType.Paint)
                ClearChosenTile();
        };
        OnTileChosen += (position, tile) => tileInfoText.text = $"{tile.name} {(Vector2)position} ";
    }
    
    private void ClearChosenTile()
    {
        TilePicker.OnTileCleared?.Invoke();
        SetNoTileText();
    }

    private void SetNoTileText()
    {
        chosenTileText.text = "No tile selected";
        chosenTileText.color = noTileSelectedColor;
    }

    private void SetChosenTileText(TileData tile)
    {
        chosenTileText.text = tile.name;
        chosenTileText.color = tileSelectedColor;
        ClickActionHandler.SetActionType(TileClickActionType.Paint);
    }
}