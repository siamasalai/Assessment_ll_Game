using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{
public void BackToMenu()
{
    SceneManager.LoadScene("MainMenu");
}
}