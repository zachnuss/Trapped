using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePurchases : MonoBehaviour
{
    //Gets access from the PlayerData to keep track of total currency owned
    public PlayerData playerData;

    //Gets access from the StoreData to keep track of purchases and everything else
    public StoreData storeData;

    //Text Variables to change the text of the store items
    public Text damagePriceText;
    public Text healthPriceText;
    public Text speedPriceText;

    //Text Variable to display the current player currency value
    public Text currentMoneyText;

    // Start is called before the first frame update
    void Start()
    {
        damagePriceText.text = "$ " + storeData.damageStartPrice;
        healthPriceText.text = "$ " + storeData.healthStartPrice;
        speedPriceText.text = "$ " + storeData.speedStartPrice;
        currentMoneyText.text = "$ " + playerData.currency;
    }
}
