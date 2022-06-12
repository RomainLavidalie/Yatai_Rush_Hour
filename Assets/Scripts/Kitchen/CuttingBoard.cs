using System;
using UnityEngine;

public class CuttingBoard : Interactable
{
    [SerializeField] private CuttableFood ingredient;
    [SerializeField] private Transform placeholder;
    
    [Header("Cutting Settings")]
    [SerializeField] private int cuttingMaxAmount = 100;
    [SerializeField] private int cuttingAmountPerClick = 10;
    [SerializeField] private int cuttingDecreaseRate = 20;
    private float currentCuttingAmount = 0;
    
    [Header("Cutting UI")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UIFiller;
    
    void Start()
    {
        UI.SetActive(false);
    }
    
    void Update()
    {
        if (ingredient != null)
        {
            if (currentCuttingAmount >= cuttingMaxAmount)
            {
                ingredient.OnCutFood();
                UI.SetActive(false);
            }
            else
            {
                currentCuttingAmount -= cuttingDecreaseRate * Time.deltaTime;
                currentCuttingAmount = Mathf.Clamp(currentCuttingAmount, 0, cuttingMaxAmount);

                UI.SetActive(currentCuttingAmount != 0);
                UIFiller.transform.localScale = new Vector3(currentCuttingAmount / cuttingMaxAmount,1, 1);
            }
        }
    }

    public override void Interact()
    {
        if (ingredient != null)
        {
            //Cut the ingredient on the board if it isn't
            if (!ingredient.isCut)
                currentCuttingAmount += cuttingAmountPerClick;
            
            //Pick it up if it is
            else
            {
                PlayerController.instance.PickUpObject(ingredient.gameObject);
                ingredient.gameObject.tag = "Pickup";
                currentCuttingAmount = 0;
                ingredient = null;
            }
        }
        
        //If there's no ingredient, put one on the board
        else
        {
            try
            {
               ingredient = PlayerController.instance.itemInHand.GetComponent<CuttableFood>();
               if (ingredient.isCut)
               {
                   ingredient = null;
                   return;
               }
               PlayerController.instance.itemInHand = null;
               var o = ingredient.gameObject;
               o.transform.parent = placeholder;
               o.transform.localPosition = Vector3.zero;
               o.tag = "Untagged";
            }
            catch
            {
                return;
            }
        }
    }
}
