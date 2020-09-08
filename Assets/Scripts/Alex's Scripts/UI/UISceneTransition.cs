using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneTransition : MonoBehaviour
{
    //Change scene to the main game scene
    public void playGame()
    {
        SceneManager.LoadScene(1);
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