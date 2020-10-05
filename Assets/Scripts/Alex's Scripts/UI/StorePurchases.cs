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
        //If statement to check if the prices are currently 0 to set them to the start price
        if (playerData.damageUpgrade == 0 && playerData.healthUpgrade == 0 && playerData.speedUpgrade == 0)
        {
            damagePriceText.text = "" + storeData.damageStartPrice;
            storeData.damagePrice = storeData.damageStartPrice;

            healthPriceText.text = "" + storeData.healthStartPrice;
            storeData.healthPrice = storeData.healthStartPrice;

            speedPriceText.text = "" + storeData.speedStartPrice;
            storeData.speedPrice = storeData.speedStartPrice;
        }

        //Else to get it to display the current price
        else
        {
            damagePriceText.text = "" + storeData.damagePrice;
            healthPriceText.text = "" + storeData.healthPrice;
            speedPriceText.text = "" + storeData.speedPrice;
        }

        //Sets the money to the current money total
        currentMoneyText.text = "" + playerData.currency;
    }

    //Functions for the store to update text when a purchase happens

    //Function for damage price adjustment
    public void damagePurchase()
    {
        //If statement to see if the upgrade can be purchased
        if (playerData.currency >= storeData.damagePrice)
        {
            playerData.UpgradeDamage(); //Calls the damage upgrade from the player data script

            playerData.currency -= storeData.damagePrice; //Subtracts the damage price from currency total
            currentMoneyText.text = "" + playerData.currency; //Sets the money to the current money total

            storeData.addDamagePrice(); //Adds to the damage price 
            damagePriceText.text = "" + storeData.damagePrice;  //Adjusts the price total
        }

        //else statement if the upgrade can't be purchased
        else
            Debug.Log("Couldn't buy the damage upgrade!");
    }

    //Function for damage price adjustment
    public void healthPurchase()
    {
        //If statement to see if the upgrade can be purchased
        if (playerData.currency >= storeData.healthPrice)
        {
            playerData.UpgradeHealth(); //Calls the health upgrade from the player data script

            playerData.currency -= storeData.healthPrice; //Subtracts the damage price from currency total
            currentMoneyText.text = "" + playerData.currency; //Sets the money to the current money total

            storeData.addHealthPrice(); //Adds to the damage price 
            healthPriceText.text = "" + storeData.healthPrice;  //Adjusts the price total
        }

        //else statement if the upgrade can't be purchased
        else
            Debug.Log("Couldn't buy the health upgrade!");

    }

    //Function for damage price adjustment
    public void speedPurchase()
    {
        //If statement to see if the upgrade can be purchased
        if (playerData.currency >= storeData.speedPrice)
        {
            playerData.UpgradeSpeed(); //Calls the speed upgrade from the player data script

            playerData.currency -= storeData.speedPrice; //Subtracts the damage price from currency total
            currentMoneyText.text = "" + playerData.currency; //Sets the money to the current money total

            storeData.addSpeedPrice(); //Adds to the damage price 
            speedPriceText.text = "" + storeData.speedPrice;  //Adjusts the price total
        }

        //else statement if the upgrade can't be purchased
        else
            Debug.Log("Couldn't buy the speed upgrade!");
    }
}
