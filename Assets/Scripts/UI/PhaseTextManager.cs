using TMPro;
using UnityEngine;


/// <summary>
/// Manage the texts that anouce every phases of the game.
/// </summary>
public class PhaseTextManager : MonoBehaviour
{
    public TMP_Text hours;
    public GameObject phaseTextParent;
    public ClockDigital clockScript;

    private string ingredientPhase;
    public string rushstart = "19:10";
    public string secondRush = "20:10";
    public string finalRush = "22:00";

    public float timeTextAppear = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //show the first phase 1sec before the start of the game
        ingredientPhase = clockScript.StartHour + ":" + (clockScript.StartMinutes + 1);
    }

    // Update is called once per frame
    void Update()
    {
        PhaseTimeCheck();
    }

    /// <summary>
    /// Check if and wich phase should be activated
    /// </summary>
    public void PhaseTimeCheck()
    {
        if (hours.text == ingredientPhase)
        {
            ActivateText(0);
            Invoke("DeactivateText", timeTextAppear);
        } 
        else if (hours.text == rushstart)
        {
            ActivateText(1);
            Invoke("DeactivateText", timeTextAppear);
            IASpawner.instance.SpawnIA(1);

        } 
        else if (hours.text == secondRush)
        {
            ActivateText(2);
            Invoke("DeactivateText", timeTextAppear);
            IASpawner.instance.SpawnIA(2);
        } 
        else if (hours.text == finalRush)
        {
            ActivateText(3);
            Invoke("DeactivateText", timeTextAppear);
            IASpawner.instance.SpawnIA(3);
        }
        
    }

    /// <summary>
    /// Activate the text
    /// </summary>
    /// <param name="childIndex"></param>
    public void ActivateText(int childIndex)
    {
        phaseTextParent.transform.GetChild(childIndex).gameObject.SetActive(true);
    }

    /// <summary>
    /// Deactivate all texts (to be sure)
    /// </summary>
    public void DeactivateText()
    {
        foreach (Transform child in phaseTextParent.transform)
        {
            child.gameObject.SetActive(false);
        }
        //phaseTextParent.transform.GetChild(childIndex).gameObject.SetActive(false);
    }
}
