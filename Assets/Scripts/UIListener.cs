/*
 * [Author: Christian Mullins]
 * [Summary: Prototype for what purpose the UIListener serves
 *      in game.]
 */ 
using UnityEngine;

public class UIListener : MonoBehaviour
{
    ///private
    private ProtoPlayerMove _playerVals;
    //values to store that will go to UI
    private int _healthUI;
    private int _scoreUI;

    void Awake()
    {
        //initialize values
        _playerVals = GameObject.FindGameObjectWithTag("Player").GetComponent<ProtoPlayerMove>();
    }

    void Update()
    {
        //get values
        _healthUI = _playerVals.health;
        _scoreUI = _playerVals.score;
    }

    private void OnGUI()
    {
        //display on screen
        GUI.Label(new Rect(10, 30, 100, 20), "Health: " + _healthUI);
        GUI.Label(new Rect(10, 10, 100, 20), "Score: " + _scoreUI);
    }
}
