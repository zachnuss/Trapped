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

    public void LoadNextLevel()
    {
        Debug.Log("Loading Next Level: " + levels[OnLevel].name);
        SceneManager.LoadScene(nextLevel.name);
    }
}
