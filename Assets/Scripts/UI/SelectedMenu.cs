using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Manage the Main menu buttons selection with the mouse.
/// </summary>
public class SelectedMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isSelected;

    public Sprite OrangeText;
    public Sprite RedText;

    // Update is called once per frame
    void Update()
    {
        Selected();
    }

    public void Selected()
    {
        if (isSelected)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.GetComponent<Image>().sprite = RedText;
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.GetComponent<Image>().sprite = OrangeText;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
    }
}
