using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }

        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}

