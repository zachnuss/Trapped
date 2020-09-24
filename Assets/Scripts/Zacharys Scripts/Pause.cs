using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Pause : MonoBehaviour
{
    bool isPaused;
    public GameObject pausePanel;


   
    public void OnPause()
    {
        if (!isPaused)
        {
            pause();

        }
        if (isPaused)
        {
            unPause();
        }



    }

    public void pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pausePanel.SetActive(true);


    }
    public void unPause()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);

    }


   
}
