using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneTransition : MonoBehaviour
{
    public PlayerData playerData;

    //Change scene to the main game scene
    public void playGame()
    {
        //SceneManager.LoadScene(1); 
        playerData.StartGame();
    }
    
    //Changes scene to the options scene
    public void options()
    {
        SceneManager.LoadScene(1);
    }

    //Changes scene to the credits scene
    public void credits()
    {
        SceneManager.LoadScene(1);
    }

    //Ends the game
    public void endGame()
    {
        //Outputs that the game is quitting for verification that the button works as expected
        Debug.Log("The Game is Quiting");

        //Code to exit out of the editor and simulate the game closing
        UnityEditor.EditorApplication.isPlaying = false;

        //Code to end the game itself below
        //Application.Quit();
    }
}

/* Scene Lists and their associated numbers
 * 
 * Main Menu = 0
 * Game = 1 - Empty scene to show the movement of stuff
 * Options - #
 * Credits - #
 * 
 */