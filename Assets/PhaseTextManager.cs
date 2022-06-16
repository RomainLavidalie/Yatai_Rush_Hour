using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
        ingredientPhase = clockScript.StartHour + ":" + (clockScript.StartMinutes + 1);
    }

    // Update is called once per frame
    void Update()
    {
        PhaseTimeCheck();
    }

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
        } 
        else if (hours.text == secondRush)
        {
            ActivateText(2);
            Invoke("DeactivateText", timeTextAppear);
        } 
        else if (hours.text == finalRush)
        {
            ActivateText(3);
            Invoke("DeactivateText", timeTextAppear);
        }
        
    }

    public void ActivateText(int childIndex)
    {
        phaseTextParent.transform.GetChild(childIndex).gameObject.SetActive(true);
    }

    public void DeactivateText()
    {
        foreach (Transform child in phaseTextParent.transform)
        {
            child.gameObject.SetActive(false);
        }
        //phaseTextParent.transform.GetChild(childIndex).gameObject.SetActive(false);
    }
}
