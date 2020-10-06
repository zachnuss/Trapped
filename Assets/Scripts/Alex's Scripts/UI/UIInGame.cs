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
    public Text ObjectiveText;
    private int objectiveTracker;
    //Image Variable to change the health bar to match the missing health percentage
    public Image healthBar; //Links to hp bar Image
    public int currHealth = 0; //Current health of the player
    public float hpBarX = 0f; //X Scale of the image bar to be set later


    //Function to keep track of the health bar removal
    public void healthBarStatus(int health)
    {
        healthText.text = "" + health; //Sets health to be displayed correctly on the HP bar
        float totalHealth = playerData.totalHealthBase; //sets a total health variable to the health base for fractioning
        float result = health / totalHealth; //Sets the fraction for the scaling 
        healthBar.rectTransform.localScale= new Vector3 ((result * hpBarX),0.38f,0.38f); //Scales the hpBar image
    }

    // Start is called before the first frame update
    void Start()
    {
        //When the scene starts it will display the current currency total that is stored in the player data
        currencyText.text = "" + playerData.currency;

        //When the scene starts it will display the current health total that is stored in the player data
        healthText.text = "" + playerData.totalHealthBase;

        hpBarX = healthBar.rectTransform.localScale.x;    
    }


    //Collision function for currency tracking
    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if the other tag is Currency
        if(other.gameObject.tag == "Currency")
        {
            Debug.Log("Got Currency!");
            playerData.currency += 100; //VARIABLE LOCATION TO CHANGE THE AMOUNT THAT CURRENCY IS WORTH *TEMP*
            Destroy(other.gameObject); //Destroys the currency obj
            currencyText.text = "" + playerData.currency; //Updates currency UI
        }

        //Checks if it was a bullet or enemy to adjust HP UI
        if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Enemy")
        {
            //Adjusts the text and image of the hp bar
            currHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().health;   //("NewPlayer").GetComponent<PlayerMovement>().health;
            healthBarStatus(currHealth);
        }
    }
    public void UpdateObjText()
    {
        objectiveTracker += 1;
        ObjectiveText.text = "-Find and press all the Yellow Buttons("+objectiveTracker.ToString()+"/5)."; 


    }




}
