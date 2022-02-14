using UnityEngine;
[RequireComponent(typeof(Food))]
public class CuttableFood : MonoBehaviour
{
    public bool isCutted = false;

    [SerializeField] private GameObject nonCuttedModel;
    [SerializeField] private GameObject cuttedModel;
    private Food foodData;

    void Start()
    {
        foodData = GetComponent<Food>();
        nonCuttedModel.SetActive(!isCutted);
        cuttedModel.SetActive(isCutted);
    }
    public void OnCutFood()
    {
        isCutted = true;
        nonCuttedModel.SetActive(false);
        cuttedModel.SetActive(true);
        foodData.IdName += "_cutted";
        foodData.ActivateCookOnCut();
    }
}
