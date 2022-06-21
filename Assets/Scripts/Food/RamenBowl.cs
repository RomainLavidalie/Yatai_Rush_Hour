using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RamenBowl : Interactable
{
    public List<string> ingredientList;
    public CapsuleCollider targetCharacter;

    private bool hasWater;
    // Start is called before the first frame update
    public override void Interact()
    {
        
        if (PlayerController.instance.itemInHand == null)
        {
            PlayerController.instance.PickUpObject(gameObject);
        }
        else
        {
            
            try
            {
                Food ingredient = PlayerController.instance.itemInHand.GetComponent<Food>();
                if (ingredient.readyForBowl)
                {
                    if(AddIngredient(ingredient))
                        Destroy(ingredient.gameObject);
                }

            }
            catch
            {

            }    
        }
    }

    private bool AddIngredient(Food ing)
    {
        if (ing.IdName == "bouillon")
        {
            Debug.Log("bouillon");
            hasWater = true;
        }
        
        if (ingredientList.Contains(ing.IdName) || !hasWater)
        {
            return false;
        }
        
        ingredientList.Add(ing.IdName);
        transform.Find(ing.IdName).gameObject.SetActive(true);
        return true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Terrain"))
        {
            ScoreText.instance.LoosePoints(100);
            Destroy(gameObject);
        }
    }
}
