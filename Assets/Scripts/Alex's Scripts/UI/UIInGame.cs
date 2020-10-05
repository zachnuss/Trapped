using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    //Gets access from the PlayerData to keep track of total currency owned
    public PlayerData playerData;

    //Text Variables to change the text of the On-Screen Items
    public Text currencyText;
    public Text healthText;


    // Start is called before the first frame update
    void Start()
    {
        //When the scene starts it will display the current currency total that is stored in the player data
        currencyText.text = "" + playerData.currency;

        //When the scene starts it will display the current health total that is stored in the player data
        healthText.text = "" + playerData.totalHealthBase;
    }


    //Collision function for currency tracking
    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if the other tag is Currency
        if(other.gameObject.tag == "Currency")
        {
            Debug.Log("Got Currency!");
            playerData.currency += 1; //VARIABLE LOCATION TO CHANGE THE AMOUNT THAT CURRENCY IS WORTH *TEMP*
            Destroy(other.gameObject); //Destroys the currency obj
            currencyText.text = "" + playerData.currency; //Updates currency UI
        }
    }

}
