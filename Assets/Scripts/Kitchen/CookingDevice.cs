using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingDevice : Interactable
{
    [HideInInspector] public enum cookingDevices
    {
        grill,
        pot
    }
    [SerializeField] private cookingDevices deviceType;

    private CookableFood ingredient;
    [SerializeField] private Transform placeholder;
    
    [Header("Cooking Settings")]
    [SerializeField] private int burningDelay = 10;
    [SerializeField] private int increaseRate = 1;
    
    private float cookingAmount;
    
    [Header("Cooking UI")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UICookFiller;
    [SerializeField] private GameObject UIBurnFiller;
    
    [SerializeField] private AudioClip cooking;
    [SerializeField] private AudioClip burning;
    [SerializeField] private AudioClip alarm;
    [SerializeField] private AudioSource source;


    void Start()
    {
        source.playOnAwake = false;
        source.loop = true;
        source.Stop();
    }
    void Update()
    {
        
        if (ingredient != null && !ingredient.isBurned && ingredient.cookingDevice == deviceType)
        {
            
            cookingAmount += increaseRate * Time.deltaTime;
            
            //If we go beyond the cooking time, the ingredient is cooked
            if (cookingAmount >= ingredient.cookingTime && !ingredient.isCooked)
                ingredient.Cook();
            
            //If we go beyond the burning delay, the ingredient is burned
            if (cookingAmount >= ingredient.cookingTime + burningDelay && !ingredient.isBurned)
                ingredient.Burn();
            
            UI.SetActive(true);
            
            //Increase the cooking bar scale depending on the cooking progression
            float cookFillerScale = Mathf.Clamp(cookingAmount / ingredient.cookingTime, 0, 1);
            UICookFiller.transform.localScale = new Vector3(cookFillerScale,1, 1);
            
            //Increase the burning bar scale depending on the burning progression
            float burnFillerScale = Mathf.Clamp((cookingAmount - ingredient.cookingTime)/burningDelay, 0, 1);
            UIBurnFiller.transform.localScale = new Vector3(burnFillerScale,1, 1);

            if (!ingredient.isCooked && source.clip != cooking)
            {
                source.clip = cooking;
                source.Play();
            }
            if (ingredient.isCooked && source.clip != burning)
            {
                source.PlayOneShot(alarm);
                source.clip = burning;
                source.Play();
            }

        }
        
        //resets all data
        else
        {
            UI.SetActive(false);
            cookingAmount = 0;
            source.Stop();
            source.clip = null;
        }
        
    }

    public override void Interact()
    {
        
        //Pickup food if it's cooked
        if (ingredient != null && ingredient.isCooked && PlayerController.instance.itemInHand == null)
        {
            PlayerController.instance.PickUpObject(ingredient.gameObject);
            ingredient.gameObject.tag = "Pickup";
            cookingAmount = 0;
            ingredient = null;
            source.Stop();
            source.clip = null;
        }
        
        if (ingredient == null)
        {
            //Start cooking the food if it's not already and matches the cooking device
            try
            {
                ingredient = PlayerController.instance.itemInHand.GetComponent<CookableFood>();
                if (ingredient.cookingDevice != deviceType || ingredient.isCooked || !ingredient.enabled)
                {
                    ingredient = null;
                }   
                else
                { 
                    PlayerController.instance.itemInHand = null;
                    var o = ingredient.gameObject;
                    o.transform.parent = placeholder;
                    o.transform.localPosition = Vector3.zero;
                    
                    //Untag the object to prevent unregistered picking up
                    o.tag = "Untagged";
                }

            }
            catch
            {
                return;
            }
        }
    }
}
