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

        GameObject go = new GameObject();
        timer = go.AddComponent<Timer>();

        timeManager.timer = timer;
    }

    private void Start()
    {
        timer.DayEvent += DayEvent;
        timer.MonthEvent += HourEvent;
    }

    private void HourEvent(object sender, TimerEventArgs time)
    {
        mapBehaviour._map.UpdatePerWeek();
    }

    private void DayEvent(object sender, TimerEventArgs time)
    {
        mapBehaviour._map.UpdatePerDay(virus);
        mapBehaviour.UpdateRegions();
    }
}
