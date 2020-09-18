//Wesley Morrison
//9/3/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTele : MonoBehaviour
{
    public PlayerData playerData;

    public GameObject teleporterTracker;//Assign before load, set to private if unneeded
    public string nextScene; //Target Level
    public Animator transition; //Transition animator
    public float transitionTime = 1;

    private void Start()
    {
        teleporterTracker = GameObject.FindGameObjectWithTag("GoalCheck"); //assumes we check on construction of the player, with a new player every level
    }



    private void OnTriggerEnter(Collider other) //Goals need to be triggers, have a trigger tag
    {
        if(other.tag == "Goal")
        {
            other.GetComponent<TeleBool>().active = true;
            if (teleporterTracker.GetComponent<TeleporterScript>().GoalCheck(teleporterTracker.GetComponent<TeleporterScript>().teleporters))
            {
                StartCoroutine(LoadTargetLevel());
            }
        }
    }

    /*public void LoadTargetLevel()
    {

        SceneManager.LoadScene(nextScene);
    }*/

    IEnumerator LoadTargetLevel()
    {
        transition.SetTrigger("Start"); //start animation

        yield return new WaitForSeconds(transitionTime); //Time given for transition animation to play

        //SceneManager.LoadScene(nextScene); //Loads target scene
        playerData.BeatLevel();
    }
}
