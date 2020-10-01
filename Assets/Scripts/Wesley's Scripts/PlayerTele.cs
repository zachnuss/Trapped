//Wesley Morrison
//9/3/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTele : MonoBehaviour
{
    public PlayerData playerData;

    public GameObject teleporterTracker;//Assign before load, set to private if unneeded
    //public string nextScene; //Target Level
    public Animator[] transition; //Transition animators > I don't think this actually matters, it isn't causing any errors > it is, if you don't have all possible transitions listed here it doesn't start the end of level anim
    public GameObject[] levelTransitions; //Reference to level transition objects  ////////////////MUST BE THE EXACT SAME AS PREVIOUS//////////////////
    public float transitionTime = 1;
    private int rng;

    private void Start()
    {
        teleporterTracker = GameObject.FindGameObjectWithTag("GoalCheck"); //assumes we check on construction of the player, with a new player every level
        rng = Random.Range(0, levelTransitions.Length);
        for (int i = 0; i < levelTransitions.Length; i++)
        {
            if (i != rng)
            {
                Image tempImage = levelTransitions[i].GetComponent<Image>();
                levelTransitions[i].GetComponent<Image>().color = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 0f);
            }
        }
        StartCoroutine(FixTransitions());
    }



    private void OnTriggerEnter(Collider other) //Goals need to be triggers, have a trigger tag
    {
        if (other.tag == "Goal")
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
        transition[rng].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime); //Time given for transition animation to play

        //SceneManager.LoadScene(nextScene); //Loads target scene
        playerData.BeatLevel();
    }

    IEnumerator FixTransitions()
    {
        //Unnecessary, changes here would break the current system. //Nvm, taking this out breaks it.
        yield return new WaitForSeconds(5);//Wait until transitions finish

        for (int i = 0; i < levelTransitions.Length; i++)
        {
            Image tempImage = levelTransitions[i].GetComponent<Image>();
            levelTransitions[i].GetComponent<Image>().color = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 1f);
        }
    }
}