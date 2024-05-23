using UnityEngine;
using UnityEngine.UI;

public class PathfindControlsUI : MonoBehaviour
{
    [SerializeField] private Button generateNewPathButton;
    
    private void Start()
    {
        generateNewPathButton.interactable = false;
        generateNewPathButton.onClick.AddListener(GenerateNewPath);
        PathfinderHandler.OnPathFinishedDrawing += EnableGenerateNewPathButton;
    }
    
    private void GenerateNewPath()
    {
        PathfinderHandler.OnNewPathRequested?.Invoke();
        generateNewPathButton.interactable = false;
    }
    
    private void EnableGenerateNewPathButton()
    {
        generateNewPathButton.interactable = true;
    }
}