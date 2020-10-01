using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneTransition : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject pauseScript;
    private void Awake()
    {
       // DontDestroyOnLoad(this);

    }
    //Change scene to the main game scene
    public void playGame()
    {
        //SceneManager.LoadScene(1); 
        playerData.StartGame();
    }

    //Changes scene to the Character Select scene
    public void charSelect()
    {
        //SceneManager.LoadScene(8);
        Debug.Log("Time to pick a character!");
    }

    //Changes scene to the options scene
    public void options()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("You have options.");
    }

    //Changes scene to the credits scene
    public void credits()
    {
        SceneManager.LoadScene(7);
        Debug.Log("We have the best team ever.");
    }

    //Ends the game
    public void endGame()
    {
        //Outputs that the game is quitting for verification that the button works as expected
        Debug.Log("The Game is Quiting");

        //Code to exit out of the editor and simulate the game closing
        //UnityEditor.EditorApplication.isPlaying = false;

        //Code to end the game itself below
        Application.Quit();
    }

    //goes back to main menu
    public void mainMenu()
    {

      

       
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

/* Scene Lists and their associated numbers
 * 
 * Main Menu = 0
 * Game = 1 - Empty scene to show the movement of stuff
 * Options - #
 * Credits - 7
 * End Scene for Playtest - 5
 * Game Over Scene - 6
 * 
 */