using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> menuOptions;
    
    public string scenePath;

    public GameObject settingsPanel;

    /// <summary>
    /// Load the Game scene
    /// </summary>
    public void OnClickPlay()
    {
        Initiate.Fade(scenePath, Color.black, 5f);
    }

    /// <summary>
    /// Open the settings panel
    /// </summary>
    public void OnClickSettings()
    {
        settingsPanel.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Close the settings panel
    /// </summary>
    public void OnClickSettingsOff()
    {
        settingsPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
