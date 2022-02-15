using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingDevice : Interactable
{
    [SerializeField] private CookableFood.cookingDevices deviceType;
    //Retirer le SerializeField lorsque les interactions seront mises en place
    [SerializeField] private CookableFood ingredient;
    
    [Header("Cooking Settings")]
    [SerializeField] private int burningDelay = 10;
    [SerializeField] private int increaseRate = 1;
    
    private float cookingAmount;
    
    [Header("Cooking UI")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UICookFiller;
    [SerializeField] private GameObject UIBurnFiller;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
            //startCooking = true;
        }
        
        if (ingredient != null && !ingredient.isBurned && ingredient.cookingDevice == deviceType)
        {
            cookingAmount += increaseRate * Time.deltaTime;
            
            //Si on atteint le temps de cuisson, l'ingrédient devient cuit
            if (cookingAmount >= ingredient.cookingTime && !ingredient.isCooked)
                ingredient.Cook();
            
            //Si on dépasse le délai de cuisson, l'ingrédient devient brulé
            if (cookingAmount >= ingredient.cookingTime + burningDelay && !ingredient.isBurned)
                ingredient.Burn();
            
            UI.SetActive(true);
            float cookFillerScale = Mathf.Clamp(cookingAmount / ingredient.cookingTime, 0, 1);
            UICookFiller.transform.localScale = new Vector3(cookFillerScale,1, 1);
            
            float burnFillerScale = Mathf.Clamp((cookingAmount - ingredient.cookingTime)/burningDelay, 0, 1);
            UIBurnFiller.transform.localScale = new Vector3(burnFillerScale,1, 1);

        }
        else
        {
            UI.SetActive(false);
            //cookingAmount = 0;
        }
        
    }

    public new void OnInteract()
    {
        /*if (ingredient != null && ingredient.isCooked)
        {
            ingredient.gameObject.transform.parent = null;
            ingredient = null; 
        }*/
        if (ingredient == null)
        {
            ingredient = GameObject.FindGameObjectWithTag("Food").GetComponent<CookableFood>();
            if (!ingredient.enabled)
            {
                ingredient = null;
            }
        }
    }
}
