using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Call this to load a specific scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Special logic for the Exit button
    public void ExitGame()
    {
        Debug.Log("Thank you for playing!");

        // This closes the app (only works in a built game)
        Application.Quit();
    }
}