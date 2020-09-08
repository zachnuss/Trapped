//Wesley Morrison
//9/3/2020
//Goes on it's own object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] teleporters; //Array of teleporters

    // Start is called before the first frame update
    void Start()
    {
        teleporters = GameObject.FindGameObjectsWithTag("Goal");//Gets the buttons in scene
    }

    public bool GoalCheck(GameObject[] targets) //Checks all the buttons, and if one of them isn't true returns false.
    {
        bool allActive = true;
        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i].GetComponent<TeleBool>().active == false)
                allActive = false;
        }
        return allActive;
    }
}
