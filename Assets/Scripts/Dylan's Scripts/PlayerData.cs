﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScritableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Player Upgrade Stats")]
    public int healthUpgrade;
    public int damageUpgrade;
    public int speedUpgrade;

    [Header("Player base stats")]
    public int totalHealthBase;
    // public int totalSpeedBase;
    public int totalDamageBase;

    [Header("Current level player is on: ZERO INDEXED")]
    public int OnLevel = 0;
    //0 = level 1 and so on

    [Header("Player score")]
    public int score = 0;

    [Header("Player Currency")]
    public int currency;

    [Header("Player Currency")]
   // public Scene[] levels;
    public string[] levelsS;

    [Header("Prev and Next")]
    //public Scene nextLevel;
    //public Scene prevLevel;
    public string nextLevelStr;
    public string prevLevelStr;

   // [Header("End and Store")]
   // public Scene endScreenScene;
    //public Scene storeScene;

    //initial setup for playerdata on lvl 1
    public void OnLevel1Load()
    {
        ResetUpgrades();
    }

    //called when level beat
    public void BeatLevel()
    {
        if (OnLevel <= levelsS.Length - 1)
        {
            Debug.Log("Beat level");
            if (OnLevel > 0)
            {
                prevLevelStr = levelsS[OnLevel];        
            }
            
            OnLevel++;
            if(OnLevel != levelsS.Length)
                nextLevelStr = levelsS[OnLevel];

            //load store scene?
            SceneManager.LoadScene("StoreScene");

        }
    }

    //load next level
    public void LoadNextLevel()
    {
        if (OnLevel != levelsS.Length)
        {
            Debug.Log("Loading Next Level: " + levelsS[OnLevel]);
            SceneManager.LoadScene(nextLevelStr);
        }
        else
            SceneManager.LoadScene(5);    
        //Debug.Log("LOAD END SCREEN HERE UWU");
    }

    //adds score
    public void AddScore(int addition)
    {
        score += addition;
    }

    //adds currency
    public void AddCurrency(int addition)
    {
        currency += addition;
        //curency adds score as well
        score += addition;
    }

    public void ResetUpgrades()
    {
        //when player starts at beginning, we reset the upgrades
        healthUpgrade = 0;
        damageUpgrade = 0;
        speedUpgrade = 0;
    }

    public void StartGame()
    {
        Debug.Log("Starting Game");
        ResetUpgrades();
        OnLevel = 0;
        SceneManager.LoadScene("Level1");
    }

    /// <summary>
    /// Upgrades are bought in the store scene, they call these 
    /// 
    /// </summary>
    /// <param name="addition"></param>

    public void UpgradeHealth()
    {
        healthUpgrade++;
    }

    public void UpgradeDamage()
    {
        damageUpgrade++;
    }

    public void UpgradeSpeed()
    {
        speedUpgrade++;
    }

    
}
