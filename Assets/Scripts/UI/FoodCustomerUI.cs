using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the RedBar of Food Orders
/// </summary>
public class FoodCustomerUI : MonoBehaviour
{
    public GameObject RedBarOrderPanel;
    public GameObject RamenOrder;
    public GameObject Separator;
    public Color color;
    private List<GameObject> UnvisibleOrders;

    // Start is called before the first frame update
    void Start()
    {
        AddRamen();
        UnvisibleOrders = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnvisibleOrders.Count > 0)
        {
            if (RedBarOrderPanel.transform.childCount < 5)
            {
                Instantiate(Separator).transform.parent = RedBarOrderPanel.transform;
                Instantiate(ColorizeOrder(RamenOrder,color)).transform.parent = RedBarOrderPanel.transform;
                UnvisibleOrders.RemoveAt(0);
            }
            
        }
    }

    /// <summary>
    /// Add a ramen order in the bar (or the list if there is no place)
    /// </summary>
    public void AddRamen()
    {
        if (RedBarOrderPanel.transform.childCount > 4)
        {
            UnvisibleOrders.Add(ColorizeOrder(RamenOrder,color));
        }
        else if (RedBarOrderPanel.transform.childCount>0 && RedBarOrderPanel.transform.childCount % 2 != 0)
        {
            Instantiate(Separator).transform.parent = RedBarOrderPanel.transform;
            Instantiate(ColorizeOrder(RamenOrder,color)).transform.parent = RedBarOrderPanel.transform;
        }
        else
        {
            Instantiate(ColorizeOrder(RamenOrder,color)).transform.parent = RedBarOrderPanel.transform;
        }
    }

    /// <summary>
    /// Remove the first order from the bar
    /// </summary>
    public void RemoveRamen()
    {
        if (RedBarOrderPanel.transform.childCount > 1)
        {
            Destroy(RedBarOrderPanel.transform.GetChild(0).gameObject);
            Destroy(RedBarOrderPanel.transform.GetChild(1).gameObject);
        }
        else
        {
            Destroy(RedBarOrderPanel.transform.GetChild(0).gameObject);
        }
        
    }

    /// <summary>
    /// Colorize the customer color indicator in the order gameObject.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="clr"></param>
    /// <returns></returns>
    public GameObject ColorizeOrder(GameObject obj, Color clr)
    {
        obj.GetComponentInChildren<Image>().color = color;
        return obj;
    }

}
