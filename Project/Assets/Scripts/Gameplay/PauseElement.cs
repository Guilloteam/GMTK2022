using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseElement : MonoBehaviour
{
    private int pauseElementCount = 0;
    void OnEnable()
    {
        pauseElementCount++;
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        pauseElementCount--;
        if(pauseElementCount <= 0)
            Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
