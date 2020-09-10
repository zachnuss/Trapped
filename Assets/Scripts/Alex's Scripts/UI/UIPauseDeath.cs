using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIPauseDeath : MonoBehaviour
{
    //Variable Initialization to access the parts of the scene for UI
    public GameObject PauseMenuUI; //Creates a slot for the pause menu
    public GameObject DeathScreenUI; //Creates a gameobject for the death screen to control when it appears

    public GameObject deathFirstButton; //Creates a gameobject to help track what button should be selected on that screen
    //Will add the optionsFirstButton, optionsClosedButton later
    
    public bool isPaused = false; //Creates a boolean to track if the pause menu should be on or off / displayed or not
    public bool hasDied = false; //Creates a boolean to track if the death menu should be on or off / displayed or not


    //CUSTOM FUNCTIONS!

    //Check for the paused button (start) to be pressed
    public void OnPause()
    {
        //Will pause the game if it isn't paused already
        if (isPaused == false)
        {
            isPaused = true;
        }

        //Will unpause the game if it is paused already
        else
            isPaused = false;
        
    }

    //Resumes the game if the back button (button East) is pressed or the resume button is selected
    public void OnReturn()
    {
        //If statement to make sure this function only works on the pause screen
        if(isPaused == true)
            isPaused = false; 
    }
    
    public void ResumeGame()
    {
        isPaused = false;
    }

    //Options menu pops up when the button is selected
    public void Options()
    {
        //*****Actual code will be put in later****
        Debug.Log("Options sub menu will pop up now!");
    }

    //Try again option will restart the games executable for the level progression
    public void TryAgain()
    {
        //*****Actual code will be put in later****
        Debug.Log("Game Resets now!");
    }

    //The player will be taken back to the main menu when that button is selected
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Function to boot up the death screen upon death
    //******Will be adjusted later with the death variable*********
    public void OnTempDeathButton()
    {
        hasDied = true;

        DeathScreenUI.SetActive(true); //Sets the Death menu to active to display it

        //Stops all background actions while the pause menu is active
        Time.timeScale = 0f;

        //Use the event system to set the appropriate button for the UI
        EventSystem.current.SetSelectedGameObject(null); //Clears the selected Obj
        EventSystem.current.SetSelectedGameObject(deathFirstButton); //Sets the selected obj
    }

    //Function to keep track of an if else statment to pause or unpause the game
    public void PauseGame()
    {
        //If function to make sure that the game only pauses when the death screen is not turned on
        if(hasDied == false)
        {
            //Checks for the pause button to be pressed
            if (isPaused == true)
            {
                PauseMenuUI.SetActive(true); //Sets the Pause menu to active to display it

                //Stops all background actions while the pause menu is active
                Time.timeScale = 0f;
            }

            //If the player selects to continue the game/resume
            else
            {
                PauseMenuUI.SetActive(false); //Sets the Pause menu to not active to hide it

                //Resumes all background actions
                Time.timeScale = 1f;
            }
        }
    }


    //MAIN CODE BELOW!

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        //When the scene starts up the pause menu and death screen will be hidden
        PauseMenuUI.SetActive(false);
        DeathScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Calls the PauseGame function to see if the game needs to be paused or not
        PauseGame();
    }
}
