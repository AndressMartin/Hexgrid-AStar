using Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileEntry : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI weightText;
    
    private TileData tileData;
    
    public void Initialize(TileData tile)
    {
        tileData = tile;
        buttonText.text = tileData.name;
        weightText.text = tileData.Weight.ToString();
        button.onClick.AddListener(OnButtonClicked);
    }
    
    private void OnButtonClicked()
    {
        TilePicker.OnTileChosen?.Invoke(tileData);
    }
}