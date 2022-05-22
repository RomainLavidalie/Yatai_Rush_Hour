using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //public string IdName;
    [SerializeField] private bool toggleCookableOnCut;
    private CookableFood cookable;
    private CuttableFood cuttable;

    private bool[] conditionsForBowl;
    private bool readyForBowl = false;

    private void Start()
    {
        cookable = GetComponent<CookableFood>();
        cuttable = GetComponent<CuttableFood>();

        conditionsForBowl = new bool[3];
        
        //Vérifier que les conditions sont réunies (2 composants existent + déclenchement activé)
        toggleCookableOnCut = (cookable != null && cuttable != null && toggleCookableOnCut);
        
        //S'il est bien activé, désactiver la possibilité de cuisiner
        if (toggleCookableOnCut)
            cookable.enabled = false;
        else
        {
            Debug.Log("manque un");
        }
    }

    public void ActivateCookOnCut()
    {
        if (toggleCookableOnCut)
            cookable.enabled = true;
    }

    public void UpdateReadyForBowl(int id)
    {
        conditionsForBowl[id] = true;
        if (conditionsForBowl[0] && conditionsForBowl[1] && !conditionsForBowl[2])
        {
            readyForBowl = true;
        }
    }
}
