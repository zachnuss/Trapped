//Wesley Morrison
//9/3/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTele : MonoBehaviour
{
    public GameObject teleporterTracker;//Assign before load, set to private if unneeded
    public string nextScene;

    //reference to playerdata
    public PlayerData playerData;

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
                //SceneManager.LoadScene(nextScene);
                playerData.BeatLevel();
                
            }
        }
    }



}
