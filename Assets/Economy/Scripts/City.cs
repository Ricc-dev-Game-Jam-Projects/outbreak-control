using System;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public float Population;
    public float Symptomatic;
    public Queue<float> Asymptomatic;
    public float Infected {
        get {
            float infected = Symptomatic;
            foreach (int a in Asymptomatic)
                infected += a / Population;
            return infected;
        }
    }
    public int Money;

    public Culture MyCulture;
    public Region Region;

    public City(Region region, Culture myCulture)
    {
        Population = 0;
        Symptomatic = 0;
        Money = 0;
        MyCulture = myCulture;
        Region = region;
        Asymptomatic = new Queue<float>();
    }

    public void UpdatePerDay(Virus virus)
    {
        float deaths = Population * Symptomatic * virus.Lethality(Region);
        Population -= deaths;
        Asymptomatic.Enqueue(Infected * virus.InfectRate(Region));
        if (Asymptomatic.Count > virus.SerialRangeRnd())
        {
            int value = Asymptomatic.Dequeue();
            if (Symptomatic == 0 && value > 0)
            {
                Region.OnRegionInfected();
            }
            Symptomatic += value;
        }
    }

    public void UpdatePerWeek()
    {
        Population *= 1.005f;
    }

    public static int MigrationPerDay(City from, City to)
    {
        float deltaPopulationDensity = from.Population /
            (from.Population + to.Population + 0.00001f);
        float deltaInfected = from.Symptomatic /
            (from.Symptomatic + to.Symptomatic + 0.00001f);
        float deltaMoney = to.Money / (from.Money + to.Money + 0.00001f);
        float migration =
            deltaMoney * 0.5f +
            deltaInfected * 0.3f +
            deltaPopulationDensity * 0.2f;

        if (migration > 0f)
            return (int)(migration * from.AbsPopulation * 0.5);
        return 0;
    }
}