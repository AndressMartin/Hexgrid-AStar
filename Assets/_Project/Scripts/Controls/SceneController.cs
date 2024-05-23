using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Button restartSceneButton;

    private void Start()
    {
        restartSceneButton.onClick.AddListener(RestartScene);
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}