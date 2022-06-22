using System;
using UnityEngine;
using Random = System.Random;

public class CuttingBoard : Interactable
{
    [SerializeField] private CuttableFood ingredient;
    [SerializeField] private Transform placeholder;
    
    [Header("Cutting Settings")]
    [SerializeField] private int cuttingMaxAmount = 100;
    //[SerializeField] private int cuttingAmountPerClick = 10;
    [SerializeField] private int cuttingDecreaseRate = 20;
    private float currentCuttingAmount = 0;
    
    [Header("Cutting UI")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UIFiller;

    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource source;
    private Random random;
    
    void Start()
    {
        UI.SetActive(false);
        random = new Random();
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
            if(PlayerController.instance.itemInHand != null)
                return;
            //Cut the ingredient on the board if it isn't
            if (!ingredient.isCut)
            {
                currentCuttingAmount += ingredient.cuttingAmountPerClick;
                source.PlayOneShot(sounds[random.Next(sounds.Length)]);
            }

            
            
            //Pick it up if it is
            else
            {
                PlayerController.instance.PickUpObject(ingredient.gameObject);
                ingredient.gameObject.GetComponent<Collider>().enabled = true;
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
               if (ingredient.isCut || !ingredient.enabled)
               {
                   ingredient = null;
                   return;
               }
               PlayerController.instance.itemInHand = null;
               var o = ingredient.gameObject;
               o.transform.parent = placeholder;
               o.transform.localPosition = Vector3.zero;
               o.GetComponent<Collider>().enabled = false;
               o.tag = "Untagged";
            }
            catch
            {
                return;
            }
        }
    }
}
