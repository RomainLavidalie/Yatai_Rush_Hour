using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookableFood : Food
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

    public void Cook()
    {
        isCooked = true;
        Debug.Log(name + ": je suis cuit");
    }
    
    public void Burn()
    {
        isBurned = true;
        Debug.Log(name + ": je crame !!!!");
    }
}
