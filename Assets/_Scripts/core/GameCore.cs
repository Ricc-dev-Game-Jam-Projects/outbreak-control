using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore instance;
    public Timer timer;

    public VirusBehaviour virusBehaviour;
    public MapBehaviour mapBehaviour;
    public TimeManager timeManager;

    private Virus virus {
        get {
            return virusBehaviour.virus;
        }
    }

    private Region region;

    void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        #endregion

        timer = new Timer();

        timeManager.timer = timer;
        timer.SecondPerReal = 1240;
        timer.Start();
    }

    private void Start()
    {
        timer.DayEvent += DayEvent;
    }

    private void DayEvent(object sender, TimerEventArgs time)
    {
        mapBehaviour._map.UpdatePerDay(virus);
        mapBehaviour.UpdateRegions();
    }
}
