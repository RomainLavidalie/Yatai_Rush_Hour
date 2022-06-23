using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Dictionary<string, GameObject> Orders;

    public static FoodCustomerUI instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Test 1");
        Orders = new Dictionary<string, GameObject>();
        Debug.Log("Test 2");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Orders.Count > 0)
        {
            if (RedBarOrderPanel.transform.childCount < 5)
            {
                Instantiate(Separator).transform.parent = RedBarOrderPanel.transform;
                Instantiate(ColorizeOrder(RamenOrder,color)).transform.parent = RedBarOrderPanel.transform;
                Orders.RemoveAt(0);
            }
        }*/
    }

    /// <summary>
    /// Add a ramen order in the bar (or the list if there is no place)
    /// </summary>
    public void AddRamen(string clientID)
    {
        if (RedBarOrderPanel.transform.childCount> 0)
        {
            GameObject separator = Instantiate(Separator, RedBarOrderPanel.transform);
            Orders.Add(clientID+"_sep", separator);
            
        }
        GameObject command = Instantiate(ColorizeOrder(RamenOrder, color), RedBarOrderPanel.transform);
        Orders.Add(clientID, command);
    }

    public void ChangeColor(Color clr)
    {
        color = clr;
    }

    /// <summary>
    /// Remove the first order from the bar
    /// </summary>
    public void RemoveRamen(string clientID)
    {
        Destroy(Orders[clientID]);
        Orders.Remove(clientID);

        try
        {
            Destroy(Orders[clientID+"_sep"]);
            Orders.Remove(clientID+"_sep");
        }
        catch
        {
            if (Orders.First().Key.Contains("_sep"))
            {
                Destroy(Orders.First().Value);
                Orders.Remove(Orders.First().Key);
            }

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
        obj.transform.Find("CustomerColor").GetComponent<Image>().color = clr;
        return obj;
    }

}
