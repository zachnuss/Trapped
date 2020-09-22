using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGet : MonoBehaviour
{

    public PlayerData playerData;
    private Text endGameScoreText;

    // Start is called before the first frame update
    void Start()
    {
        endGameScoreText = gameObject.GetComponent<Text>();
        endGameScoreText.text = "Final Score: " + playerData.score;
    }
    
}
