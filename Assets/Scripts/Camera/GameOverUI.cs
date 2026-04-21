using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // nombre exacto de tu escena del menú principal
    public string mainMenuSceneName = "Menu";

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // índice 0 = Scenes/Menu según tu Build Settings
    }

    public void QuitGame()
    {
        Application.Quit();
        // esto solo funciona en build, en el editor usa:
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
