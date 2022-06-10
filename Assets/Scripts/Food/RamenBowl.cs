using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RamenBowl : Interactable
{
    public List<string> ingredientList;
    public bool isComplete;
    
    // Start is called before the first frame update
    public override void Interact()
    {
        if (PlayerController.instance.itemInHand == null && isComplete)
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
                    AddIngredient(ingredient);
                    Destroy(ingredient.gameObject);
                }

            }
            catch
            {

            }    
        }
    }

    private void AddIngredient(Food ing)
    {
        if (ingredientList.Contains(ing.name))
        {
            return;
        }
        ingredientList.Add(ing.IdName);
        transform.Find(ing.IdName).gameObject.SetActive(true);

        foreach (Recipe validRecipe in RecipesManager.instance.recipesList)
        {
            bool isEqual = Enumerable.SequenceEqual(ingredientList.OrderBy(e => e), validRecipe.ingredients.OrderBy(e => e));
            if (isEqual)
            {
                isComplete = true;
                break;
            }
        }
        
        
    }
}
