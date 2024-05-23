using UnityEngine;
using UnityEngine.UI;

public class TilePickerUI : MonoBehaviour
{
    [SerializeField] private Button clearChoiceButton;
    
    private void Start()
    {
        clearChoiceButton.onClick.AddListener(ClearChosenTile);
    }
    
    private void ClearChosenTile()
    {
        TilePicker.OnTileCleared?.Invoke();
    }
}