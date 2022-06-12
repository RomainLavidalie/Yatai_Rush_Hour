using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public Recipe command;
    public void OnTriggerEnter(Collider other)
    {
        List<string> recieved = other.GetComponent<RamenBowl>().ingredientList;
    }
}
