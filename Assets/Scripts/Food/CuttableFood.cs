using UnityEngine;
[RequireComponent(typeof(Food))]
public class CuttableFood : MonoBehaviour
{
    [SerializeField] private GameObject nonCutModel;
    [SerializeField] private GameObject cutModel;
    public float cuttingAmountPerClick;
    private Food foodData;

    public bool isCut;

    void Start()
    {
        foodData = GetComponent<Food>();
        
        cutModel.SetActive(false);
        nonCutModel.SetActive(true);
    }
    public void OnCutFood()
    {
        isCut = true; 
        
        foodData.UpdateReadyForBowl(0);
        foodData.ActivateCookOnCut();
        
        nonCutModel.SetActive(false);
        cutModel.SetActive(true);
    }
}
