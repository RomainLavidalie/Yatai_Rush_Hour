using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingDevice : Interactable
{
    [SerializeField] private CookableFood.cookingDevices deviceType;
    //Retirer le SerializeField lorsque les interactions seront mises en place
    [SerializeField] private CookableFood ingredient;
    
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UICookFiller;
    [SerializeField] private GameObject UIBurnFiller;

    private float cookingAmount;
    [SerializeField] private int burningDelay = 10;
    [SerializeField] private int increaseRate = 1;

    private bool startCooking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            startCooking = true;
        }
        
        if (ingredient != null && !ingredient.isBurned && startCooking && ingredient.cookingDevice == deviceType)
        {
            cookingAmount += increaseRate * Time.deltaTime;
            
            //Si on atteint le temps de cuisson, l'ingrédient est cuit
            if (cookingAmount >= ingredient.cookingTime)
                ingredient.Cook();
            
            //Si on dépasse le délai de cuisson, l'ingrédient est brulé
            if (cookingAmount >= ingredient.cookingTime + burningDelay)
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
        if (ingredient != null && ingredient.isCooked)
        {
            ingredient.gameObject.transform.parent = null;
            ingredient = null; 
        }

    }
}
