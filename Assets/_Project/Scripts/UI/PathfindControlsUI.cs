using UnityEngine;
using UnityEngine.UI;

public class PathfindControlsUI : MonoBehaviour
{
    [SerializeField] private Button generateNewPathButton;
    
    private void Start()
    {
        generateNewPathButton.onClick.AddListener(GenerateNewPath);
    }
    
    private void GenerateNewPath()
    {
        PathfinderHandler.OnNewPathRequested?.Invoke();
    }
}