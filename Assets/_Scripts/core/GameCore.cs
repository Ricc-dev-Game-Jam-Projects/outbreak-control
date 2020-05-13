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
        Region[,] r = mapBehaviour._map.Grid;
        int i, j;
        i = Random.Range(0, mapBehaviour._map.Width - 1);
        j = Random.Range(0, mapBehaviour._map.Height - 1);
        while(region == null)
        {
            region = r[i, j];
            if(region.Type == RegionType.Water)
            {
                region = null;
                i = Random.Range(0, mapBehaviour._map.Width - 1);
                j = Random.Range(0, mapBehaviour._map.Height - 1);
            }
        }
        region.city.Infected = 1;
        timer.DayEvent += DayEvent;
    }

    int lastDayOfContagion = 1;

    private void DayEvent(object sender, TimerEventArgs time)
    {
        Debug.Log("Log de virus, dia " + time.timer.Day);
        if(lastDayOfContagion >= virus.SerialRange)
        {
            lastDayOfContagion = 0;
            
            float spread = virus.InfectRate(region);
            float lethality = virus.Lethality(region);
            Debug.Log("Spread " + spread);
            Debug.Log("Lethality " + lethality);
            float previousInfected = region.city.Infected;
            int previousPop = region.city.PopulationSize;

            region.city.Infected += spread;
            System.Random random = new System.Random();

            float valor = (float)random.NextDouble();
            float pog = region.city.PopulationSize * (1f - valor * lethality);

            region.city.PopulationSize = (int)pog;

            Debug.Log("Número de contaminados passou de " + previousInfected + " a ser " + region.city.Infected);
            Debug.Log("Número de mortes hoje foi de "
                        + (previousPop - region.city.PopulationSize) + " restando "
                        + region.city.PopulationSize + " pessoas");
        }
        lastDayOfContagion++;
    }
}
