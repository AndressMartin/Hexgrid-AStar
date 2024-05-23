using System;
using Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilePickerUI : MonoBehaviour
{
    [SerializeField] private Button clearChoiceButton;
    [SerializeField] private TextMeshProUGUI chosenTileText;
    
    private void Start()
    {
        clearChoiceButton.onClick.AddListener(ClearChosenTile);
        TilePicker.OnTileChosen += SetChosenTileText;
    }
    
    private void ClearChosenTile()
    {
        TilePicker.OnTileCleared?.Invoke();
        chosenTileText.text = string.Empty;
    }
    
    private void SetChosenTileText(TileData tile)
    {
        chosenTileText.text = tile.name;
    }
}