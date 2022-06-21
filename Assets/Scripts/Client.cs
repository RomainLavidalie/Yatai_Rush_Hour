using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using Random = System.Random;

public class Client : MonoBehaviour
{
    public Recipe command;
    private List<string> recievedBowl;
    [SerializeField] IAStateMachine stateMachine;

    private void Start()
    {
        Random rand = new Random();
        command = RecipesManager.instance.recipesList[rand.Next(RecipesManager.instance.recipesList.Count)];
    }

    public void OnCollisionEnter(Collision other)
    {
        try
        {
            recievedBowl = other.collider.GetComponent<RamenBowl>().ingredientList;
        }
        catch
        {
            return;
        }

        if (Enumerable.SequenceEqual(recievedBowl.OrderBy(e => e), command.ingredients.OrderBy(e => e)))
            Happy();
        else
            Angry();
        Destroy(other.gameObject);
    }

    private void Happy()
    {
        Debug.Log("bonne commande");
        stateMachine._orderArrived = true;
        
    }

    private void Angry()
    {
        Debug.Log("mauvaise commande");
        
    }
}
