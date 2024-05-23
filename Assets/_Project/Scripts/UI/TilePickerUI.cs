using System;
using Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilePickerUI : MonoBehaviour
{
    [SerializeField] private Button clearChoiceButton;
    [SerializeField] private TextMeshProUGUI chosenTileText;
    
    [Header("Text Colors")]
    [SerializeField] private Color noTileSelectedColor;
    [SerializeField] private Color tileSelectedColor;
    
    private void Start()
    {
        SetNoTileText();
        clearChoiceButton.onClick.AddListener(ClearChosenTile);
        TilePicker.OnTileChosen += SetChosenTileText;
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
    }
}