using System;
using UnityEngine;
[RequireComponent(typeof(Food))]
public class CookableFood : MonoBehaviour
{
    public CookingDevice.cookingDevices cookingDevice;
    public int cookingTime;
    
    [SerializeField] private GameObject nonCookedModel;
    [SerializeField] private GameObject cookedModel;
    [SerializeField] private GameObject burnedModel;

    public bool isCooked;
    public bool isBurned;
    
    private Food foodData;
    public void Start()
    {
        foodData = GetComponent<Food>();
        
        cookedModel.SetActive(false);
        burnedModel.SetActive(false);
        nonCookedModel.SetActive(true);
    }

    public void Cook()
    {
        isCooked = true;
        foodData.UpdateReadyForBowl(1);
        
        nonCookedModel.SetActive(false);
        burnedModel.SetActive(false);
        cookedModel.SetActive(true);
    }
    
    public void Burn()
    {
        isBurned = true;
        foodData.UpdateReadyForBowl(2);
        
        nonCookedModel.SetActive(false);
        cookedModel.SetActive(false);
        burnedModel.SetActive(true);
    }
}
