
using UnityEngine;

public class CuttingBoard : Interactable
{
    
    
    [SerializeField] private CuttableFood ingredient;
    
    [SerializeField] private int cuttingMaxAmount = 100;
    [SerializeField] private int cuttingAmountPerClick = 10;
    [SerializeField] private int cuttingDecreaseRate = 20;
    private float currentCuttingAmount = 0;
    
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UIFiller;
    
    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Supprimer cette partie une fois que le contrôleur sera mis en place
        if (Input.GetButtonDown("Fire1"))
        {
            OnInteract();
        }

        if (ingredient != null)
        {
            if (currentCuttingAmount >= cuttingMaxAmount)
            {
                ingredient.OnCutFood();
                UI.SetActive(false);
            }
            else
            {
                currentCuttingAmount -= cuttingDecreaseRate * Time.deltaTime;
                currentCuttingAmount = Mathf.Clamp(currentCuttingAmount, 0, cuttingMaxAmount);
                //Debug.Log(currentCuttingAmount);
                UI.SetActive(currentCuttingAmount != 0);
                UIFiller.transform.localScale = new Vector3(currentCuttingAmount / cuttingMaxAmount,1, 1);
            }
        }
    }

    public new void OnInteract()
    {
        if (ingredient != null)
        {
            if (!ingredient.isCutted)
                currentCuttingAmount += cuttingAmountPerClick;
            else
            {
                //Pick up object if holding nothing
            }
        }
        else
        {
            //Put object on the board, if holding one
        }
    }
}
