using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Reload the scene
    /// </summary>
    public void OnClickRetry()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
