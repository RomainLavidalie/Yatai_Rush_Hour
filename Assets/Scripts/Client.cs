using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Client : MonoBehaviour
{
    public Recipe command;
    private List<string> recievedBowl;
    [SerializeField] IAStateMachine stateMachine;
    [SerializeField] private float timeBonusPoints;
    private Random rand;


    private void Update()
    {
        timeBonusPoints = Mathf.Max(0, timeBonusPoints - Time.deltaTime);
        //timeBonusPoints -= Time.deltaTime;
    }

    public void OrderFood()
    {
        rand = new Random();
        command = RecipesManager.instance.recipesList[rand.Next(RecipesManager.instance.recipesList.Count)];
        timeBonusPoints = 50;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Pickup"))
        {
            Angry(50);
            Destroy(other.gameObject);
            return;
        }
        try
        {
            recievedBowl = other.collider.GetComponent<RamenBowl>().ingredientList;
        }
        catch
        {
            return;
        }
        
        Destroy(other.gameObject);
        
        if (command == null)
        {
            Angry(50);
            return;
        }

        if (Enumerable.SequenceEqual(recievedBowl.OrderBy(e => e), command.ingredients.OrderBy(e => e)))
        {
            int priorityMult = 1;
            if (stateMachine._foodCustomerUI.Orders.First().Key == gameObject.name)
                priorityMult = 3;
            Happy(100 + Mathf.RoundToInt(timeBonusPoints)*priorityMult);
        }

        else
            Angry(100);

    }

    private void Happy(int win)
    {
        Debug.Log("bonne commande");
        stateMachine._orderArrived = true;
        command = null;
        ScoreText.instance.IncrementScore(win);
        
    }

    private void Angry(int loose)
    {
        Debug.Log("mauvaise commande");
        ScoreText.instance.LoosePoints(loose);
    }
}
