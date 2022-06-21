using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public ClockDigital Clock;
    public AudioSource musicSource;
    
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

    public void OnClickContinue()
    {
        if (PauseMenu.activeSelf && !Clock.IsGameOver)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            musicSource.Play();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            musicSource.Pause();
        }
    }
}
