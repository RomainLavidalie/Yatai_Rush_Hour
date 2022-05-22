using System;
using UnityEngine;
[RequireComponent(typeof(Food))]
public class CookableFood : MonoBehaviour
{

    [HideInInspector] public enum cookingDevices
    {
        grill,
        pot
    }
    
    public cookingDevices cookingDevice;
    public int cookingTime;
    
    public bool isCooked;
    public bool isBurned;
    
    private Food foodData;
    public void Start()
    {
        foodData = GetComponent<Food>();
    }

    public void Cook()
    {
        isCooked = true;
        Debug.Log(name + ": je suis cuit");
        foodData.UpdateReadyForBowl(1);
    }
    
    public void Burn()
    {
        isBurned = true;
        Debug.Log(name + ": je crame !!!!");
        foodData.UpdateReadyForBowl(2);
    }
}
