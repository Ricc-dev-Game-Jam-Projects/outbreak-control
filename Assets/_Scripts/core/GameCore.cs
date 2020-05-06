using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public Timer timer;

    public TimeManager timeManager;

    void Awake()
    {
        timer = new Timer();

        timeManager.timer = timer;
        timer.SecondPerReal = 1240;
        timer.Start();
    }

    void Update()
    {
        
    }
}
