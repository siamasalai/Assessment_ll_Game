using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // --- RUN THIS FROM THE MAIN MENU SCENE ---
    public void OpenCredits()
    {
        // Loads the separate credits scene
        SceneManager.LoadScene("Credits");
    }

    public void OpenTutorials()
    {
        // Loads the separate tutorials scene
        SceneManager.LoadScene("Tutorial");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Start");
    }

    // --- RUN THIS FROM THE CREDITS/TUTORIAL SCENE (Attached to the Back Button) ---
    public void BackToMenu()
    {
        // Loads your main menu scene back up
        SceneManager.LoadScene("Menu");
    }

    // --- NEW: EXIT THE APPLICATION ---
    public void QuitGame()
    {
        Debug.Log("Game is exiting...");

#if UNITY_EDITOR
        // If we are in the Unity Editor, stop the play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If we are in the built application, close it
            Application.Quit();
#endif
    }
}

