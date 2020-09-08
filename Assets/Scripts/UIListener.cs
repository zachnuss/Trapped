/*
 * [Author: Christian Mullins]
 * [Summary: Prototype for what purpose the UIListener serves
 *      in game.]
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIListener : MonoBehaviour
{
    ///private
    //values to store that will go to UI
    private int _healthUI;
    private int _scoreUI;
    private bool _isGameOver;
    private int _gameOverInt;

    void Awake()
    {
        /**
         * NOTE: Make sure the GameOverScene is the final scene in the build index
         */ 
        _gameOverInt = SceneManager.sceneCountInBuildSettings - 1;
        _isGameOver = (SceneManager.GetActiveScene().name == "GameOverScene");

        //is this the GameOverScene?
        if (_isGameOver)
        {
            Text finalScore = GameObject.Find("EndGameScoreText").GetComponent<Text>();
            finalScore.text = "Final Score:\n" + ProtoPlayerMove.score;
        }
    }

    void Update()
    {

        if (_isGameOver) return;

        //get values
        _healthUI = ProtoPlayerMove.health;
        _scoreUI = ProtoPlayerMove.score;
    }

    /**
     * OnGUI() will be removed once finalized UI has been made.
     */ 
    private void OnGUI()
    {
        if (!_isGameOver)
        {
            //display on screen
            GUI.Label(new Rect(10, 30, 100, 20), "Health: " + _healthUI);
            GUI.Label(new Rect(10, 10, 100, 20), "Score: " + _scoreUI);
        }
    }
}
