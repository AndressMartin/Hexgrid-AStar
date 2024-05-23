using UnityEngine;
using UnityEngine.UI;

public class PathfindControlsUI : MonoBehaviour
{
    [SerializeField] private Button generateNewPathButton;
    [SerializeField] private Button setStartButton;
    [SerializeField] private Button setEndButton;
    
    private void Start()
    {
        generateNewPathButton.interactable = false;
        generateNewPathButton.onClick.AddListener(GenerateNewPath);
        setStartButton.onClick.AddListener(() => ClickActionHandler.SetActionType(TileClickActionType.SetStart));
        setEndButton.onClick.AddListener(() => ClickActionHandler.SetActionType(TileClickActionType.SetEnd));
        PathfinderHandler.OnPathFinishedDrawing += EnableGenerateNewPathButton;
    }
    
    private void GenerateNewPath()
    {
        PathfinderHandler.OnNewPathRequested?.Invoke();
        generateNewPathButton.interactable = false;
        ClickActionHandler.SetActionType(TileClickActionType.None);
    }
    
    private void EnableGenerateNewPathButton()
    {
        generateNewPathButton.interactable = true;
    }
}