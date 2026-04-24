using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("Drag your Farewell Panel here")]
    public GameObject farewellPanel;

   
    void Start()
    {
        // Check if we are starting in the Exit scene to trigger the timer
        if (SceneManager.GetActiveScene().name == "Exit")
        {
            StartCoroutine(ExitSequence());
        }
    }

    // --- NAVIGATION METHODS ---
    public void PlayGame()
    {
        SceneManager.LoadScene("MySimulation");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credit");
    }

    public void OpenTutorials()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // --- EXIT LOGIC ---
    public void QuitGame()
    {
        // Run sequence if in Exit scene, otherwise load the Exit scene
        if (SceneManager.GetActiveScene().name == "Exit")
        {
            StartCoroutine(ExitSequence());
        }
        else
        {
            SceneManager.LoadScene("Exit");
        }
    }

    IEnumerator ExitSequence()
    {
        Debug.Log("Exit sequence triggered...");

        // 1. Show Farewell UI
        if (farewellPanel != null)
        {
            farewellPanel.SetActive(true);
        }

        // 2. Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // 3. Platform Handling
#if UNITY_EDITOR
        // Stops the editor play mode
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        // Redirect for WebGL builds
        Application.OpenURL("https://itch.io/dashboard"); 
#else
        // Standard close for .exe builds
        Application.Quit();
#endif
    }
}