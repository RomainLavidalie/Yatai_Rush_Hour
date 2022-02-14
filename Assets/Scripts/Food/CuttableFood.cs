using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableFood : Food
{
    public bool isCutted = false;

    [SerializeField] private GameObject nonCuttedModel;
    [SerializeField] private GameObject cuttedModel;
    // Start is called before the first frame update
    void Start()
    {
        nonCuttedModel.SetActive(!isCutted);
        cuttedModel.SetActive(isCutted);
    }
    public void OnCutFood()
    {
        isCutted = true;
        nonCuttedModel.SetActive(false);
        cuttedModel.SetActive(true);
    }
}
