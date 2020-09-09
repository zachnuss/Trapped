using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIPauseDeath : MonoBehaviour
{
    //Variable Initialization to access the parts of the scene for UI
    public GameObject PauseMenuUI; //Creates a slot for the pause menu

    public GameObject pauseFirstButton; //Public Gameobject to set the first button highlight selection in the pause menu
    //Will add the optionsFirstButton, optionsClosedButton later
    
    public bool isPaused = false; //Creates a boolean to track if the pause menu should be on or off / displayed or not


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

    //The player will be taken back to the main menu when that button is selected
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Function to keep track of an if else statment to pause or unpause the game
    public void PauseGame()
    {
        //Checks for the pause button to be pressed
        if (isPaused == true)
        {
            PauseMenuUI.SetActive(true); //Sets the Pause menu to active to display it

            //Stops all background actions while the pause menu is active
            Time.timeScale = 0f;

            //Clear the Resume Button to reselect it after and highlight it
            //EventSystem.current.SetSelectedGameObject(null); //Clears selected gameobject
            //EventSystem.current.SetSelectedGameObject(pauseFirstButton); //Sets the button to Resume to highlight it
        }

        //If the player selects to continue the game/resume
        else
        {
            PauseMenuUI.SetActive(false); //Sets the Pause menu to not active to hide it

            //Resumes all background actions
            Time.timeScale = 1f;
        }
    }


    //MAIN CODE BELOW!

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        //When the scene starts up the pause screen will be hidden
        PauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Calls the PauseGame function to see if the game needs to be paused or not
        PauseGame();
    }
}
