using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create a scritable object to keep track of the Store Data to adjust prices and display purchases easier

[CreateAssetMenu(fileName = "StoreData", menuName = "ScritableObjects/StoreData", order = 2)]

public class StoreData : ScriptableObject
{
    //Creates a header to show that this is the price points per upgrade
    [Header("Begging Store Prices")]
    public int damageStartPrice;
    public int healthStartPrice;
    public int speedStartPrice;



    //Functions to keep track of the storeData

    //Sets up the initial 
}
