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
    public Scene[] levels;

    public Scene nextLevel;
    public Scene prevLevel;

    public Scene endScreenScene;
    public Scene storeScene;

    public void OnLevel1Load()
    {
        ResetUpgrades();
    }

    //called when level beat
    public void BeatLevel()
    {
        if (OnLevel <= levels.Length - 1)
        {
            prevLevel = levels[OnLevel];
            OnLevel++;
            nextLevel = levels[OnLevel];

            //load store scene?

        }
        else
        {
            Debug.Log("You did it you won. Congradulations.");

        }
    }

    //load next level
    public void LoadNextLevel()
    {
        Debug.Log("Loading Next Level: " + levels[OnLevel].name);
        SceneManager.LoadScene(nextLevel.name);
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
