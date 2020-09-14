using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScritableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public int healthUpgrade;
    public int damageUpgrade;
    public int speedUpgrade;


    private int totalHealth;
    private int totalSpeed;
    private int totalDamage;

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

    //called when level beat
    public void BeatLevel()
    {
        prevLevel = levels[OnLevel];
        OnLevel++;
        nextLevel = levels[OnLevel];

        //load store scene?
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
