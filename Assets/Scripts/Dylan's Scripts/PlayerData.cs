using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScritableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Current level player is on: ZERO INDEXED")]
    public int OnLevel = 0;
    //0 = level 1 and so on

    [Header("Player score")]
    public int score = 0;

    [Header("Player Currency")]
    public int currency;

    [Header("Player Currency")]
    public Scene[] levelNames;



    //called when level beat
    public void BeatLevel()
    {
        OnLevel++;

    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading Next Level: " + levelNames[OnLevel].name);
        SceneManager.LoadScene(levelNames[OnLevel].name);
    }
}
