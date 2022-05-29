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
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(scenePath);
    }

    public void OnClickSettings()
    {
        settingsPanel.gameObject.SetActive(true);
    }
    
    public void OnClickSettingsOff()
    {
        settingsPanel.gameObject.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
