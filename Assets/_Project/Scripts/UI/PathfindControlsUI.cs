using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PathfindControlsUI : MonoBehaviour
{
    [SerializeField] private Button generateNewPathButton;
    [SerializeField] private Button setStartButton;
    [SerializeField] private Button setEndButton;
    [SerializeField] private TextMeshProUGUI pathStatusText;
    [SerializeField] private Color pathFailedColor;
    
    private void Start()
    {
        generateNewPathButton.interactable = false;
        generateNewPathButton.onClick.AddListener(GenerateNewPath);
        setStartButton.onClick.AddListener(() => ClickActionHandler.SetActionType(TileClickActionType.SetStart));
        setEndButton.onClick.AddListener(() => ClickActionHandler.SetActionType(TileClickActionType.SetEnd));
        PathfinderHandler.OnPathFinishedDrawing += EnableGenerateNewPathButton;
        HexPathfinder.OnPathSuccess += SetPathSuccessText;
        HexPathfinder.OnPathFailed += SetPathFailedText;
    }
    
    private void GenerateNewPath()
    {
        generateNewPathButton.interactable = false;
        ClickActionHandler.SetActionType(TileClickActionType.None);
        PathfinderHandler.OnNewPathRequested?.Invoke();
    }
    
    private void EnableGenerateNewPathButton()
    {
        generateNewPathButton.interactable = true;
    }
    
    private void SetPathSuccessText()
    {
        pathStatusText.text = string.Empty;
    }
    
    private void SetPathFailedText()
    {
        pathStatusText.text = "Path not found";
        pathStatusText.color = pathFailedColor;
        generateNewPathButton.interactable = true;
    }
}