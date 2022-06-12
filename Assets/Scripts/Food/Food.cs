using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public string IdName;
    [SerializeField] private bool toggleCookableOnCut;
    
    private CookableFood cookable;
    private CuttableFood cuttable;

    private bool[] conditionsForBowl;
    public bool readyForBowl = false;

    private void Start()
    {
        cookable = GetComponent<CookableFood>();
        cuttable = GetComponent<CuttableFood>();

        conditionsForBowl = new bool[3];
        
        //Set corresponding bool tu true if the ingredient can't be cut/cooked
        conditionsForBowl[0] = cuttable == null;
        conditionsForBowl[1] = cookable == null;
        
        //The toggle will only work if the ingredient can be both cut and cooked, and if it's activated itself
        toggleCookableOnCut = (cookable != null && cuttable != null && toggleCookableOnCut);
        
        //Disable the possibility to cook
        if (toggleCookableOnCut)
            cookable.enabled = false;

    }

    public void ActivateCookOnCut()
    {
        //Enable the possibility to cook if it depends of the toggle
        if (toggleCookableOnCut)
            cookable.enabled = true;
    }

    public void UpdateReadyForBowl(int id)
    {
        //Checks a condition and verify if the food is ready for the bowl (cut, cooked and not burned)
        conditionsForBowl[id] = true;
        readyForBowl = (conditionsForBowl[0] && conditionsForBowl[1] && !conditionsForBowl[2]);
    }
}
