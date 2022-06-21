using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

        if (Enumerable.SequenceEqual(recievedBowl.OrderBy(e => e), command.ingredients.OrderBy(e => e)))
            Happy(100);
        else
            Angry(100);
        Destroy(other.gameObject);
    }

    private void Happy(int win)
    {
        Debug.Log("bonne commande");
        stateMachine._orderArrived = true;
        ScoreText.instance.IncrementScore(100);
        
    }

    private void Angry(int loose)
    {
        Debug.Log("mauvaise commande");
        ScoreText.instance.LoosePoints(loose);
    }
}
