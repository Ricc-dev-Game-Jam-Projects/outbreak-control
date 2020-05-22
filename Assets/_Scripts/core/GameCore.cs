using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameCore : MonoBehaviour
{
    public static GameCore instance;
    public Timer timer;

    [Header("UI Behaviours")]
    public MapUI mapUI;
    public VirusUI virusUI;
    [Space()]
    [Header("Behaviours")]
    public VirusBehaviour virusBehaviour;
    public MapBehaviour mapBehaviour;
    public TimeManager timeManager;
    public IBehaviour teste;

    private Virus virus {
        get {
            return virusBehaviour.virus;
        }
    }

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

        // Cria todos os behaviours

        GameObject go = new GameObject();
        timer = go.AddComponent<Timer>();

        timeManager.timer = timer;
    }

    private void Start()
    {
        timer.DayEvent += DayEvent;
        timer.MonthEvent += MonthEvent;
    }

    private void MonthEvent(object sender, TimerEventArgs time)
    {
        mapBehaviour.map.UpdatePerWeek();
    }

    private void DayEvent(object sender, TimerEventArgs time)
    {
        mapBehaviour.map.UpdatePerDay(virus);
        mapBehaviour.UpdateRegions();
    }
}
