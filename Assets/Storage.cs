using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Interactable
{
    [SerializeField] private GameObject objectToGenerate;
    public override void Interact()
    {
        if (PlayerController.instance.itemInHand == null)
        {
            GameObject obj = Instantiate(objectToGenerate);
            PlayerController.instance.PickUpObject(obj);
        }
    }
}
