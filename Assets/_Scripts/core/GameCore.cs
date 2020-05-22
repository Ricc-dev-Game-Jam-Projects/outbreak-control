using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameCore : MonoBehaviour
{
    public static GameCore instance;

    [Header("UI Behaviours")]
    public MapUI mapUI;
    public VirusUI virusUI;
    public TimeManager timeUI;

    [Space()]
    [Header("Behaviours")]
    public VirusBehaviour virusBehaviour;
    public MapBehaviour mapBehaviour;
    public TrumpBehaviour trumpBehaviour;

    [Space()]
    [Header("Prefabs")]
    public GameObject VirusPopperPrefab;
    public GameObject RegionPrefab;
    public GameObject WallPrefab;

    public List<IBehaviour> Behaviours;
    public List<IBehaviour> UIBehaviours;

    [HideInInspector]
    public Timer timer;

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

        UIBehaviours = new List<IBehaviour>();
        Behaviours = new List<IBehaviour>();

        // Add UI Behaviours
        UIBehaviours.Add(mapUI as IBehaviour);

        // Cria todos os behaviours
        GameObject virusObj = CreateObj("VirusBehaviour");
        virusBehaviour = virusObj.AddComponent<VirusBehaviour>();
        virusBehaviour.virusPopperPrefab = VirusPopperPrefab;
        virusBehaviour._VirusUI = virusUI;

        GameObject mapObj = CreateObj("MapBehaviour");
        mapBehaviour = mapObj.AddComponent<MapBehaviour>();
        mapBehaviour.RegionPrefab = RegionPrefab;
        mapBehaviour.WGrid = 41;
        mapBehaviour.HGrid = 27;
        mapBehaviour.transform.position = new Vector3(-8.881f, -5f);

        GameObject wallObj = CreateObj("TrumpBehaviour");
        trumpBehaviour = wallObj.AddComponent<TrumpBehaviour>();
        trumpBehaviour.WallPrefab = WallPrefab;

        AddBehaviour(mapBehaviour as IBehaviour);
        AddBehaviour(virusBehaviour as IBehaviour);
        AddBehaviour(trumpBehaviour as IBehaviour);

        GameObject timerManager = CreateObj("TimerManager");
        timer = timerManager.AddComponent<Timer>();

        timeUI.timer = timer;

        // Run Logic Behaviours first
        foreach (IBehaviour behaviour in Behaviours)
        {
            behaviour.MyAwake();
        }

        foreach (IBehaviour behaviour in Behaviours)
        {
            behaviour.MyStart();
        }

        // Run UI Behaviours second
        foreach (IBehaviour behaviour in UIBehaviours)
        {
            behaviour.MyAwake();
        }

        foreach (IBehaviour behaviour in UIBehaviours)
        {
            behaviour.MyStart();
        }
    }

    public GameObject CreateObj(string name)
    {
        GameObject Obj = new GameObject();
        Obj.name = name;
        Obj.transform.parent = gameObject.transform;
        return Obj;
    }

    public void AddBehaviour(IBehaviour behaviour)
    {
        if (behaviour != null)
        {
            Behaviours.Add(behaviour);
        }
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
