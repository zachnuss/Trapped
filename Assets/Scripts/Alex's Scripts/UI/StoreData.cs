using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create a scritable object to keep track of the Store Data to adjust prices and display purchases easier

[CreateAssetMenu(fileName = "StoreData", menuName = "ScritableObjects/StoreData", order = 2)]

public class StoreData : ScriptableObject
{
    //Creates a header to show that this is the price points per upgrade and the price points associated with them
    [Header("Begging Store Prices")]
    public int damageStartPrice;
    public int healthStartPrice;
    public int speedStartPrice;

    //Creates a header to show that this is the price points per upgrade and the price points associated with them
    [Header("Current Store Prices")]
    public int damagePrice;
    public int healthPrice;
    public int speedPrice;

    //Creates a header to show that this is the price Addition per upgrade level and the points associated with them
    [Header("Store Additions")]
    public int damageAddition;
    public int healthAddition;
    public int speedAddition;


    //Functions to keep track of the storeData


    //Functions to add the additions to the price when an upgrade is purchased
    public void addDamagePrice()
    {
            damagePrice += damageAddition;
    }

    //Functions to add the additions to the price when an upgrade is purchased
    public void addHealthPrice()
    {
        healthPrice += healthAddition;
    }

    //Functions to add the additions to the price when an upgrade is purchased
    public void addSpeedPrice()
    {
        speedPrice += speedAddition;
    }
}
