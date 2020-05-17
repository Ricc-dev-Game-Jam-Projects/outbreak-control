using System;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public static int TotalPopulation = 0;
    public const float Person = 1e-3f;
    public float Population {
        get { return NotInfected + Infected; }
    }
    public float Symptomatic;
    public Queue<float> Asymptomatic;
    public float Infected {
        get {
            float infected = Symptomatic;
            foreach (float a in Asymptomatic)
                infected += a;
            return infected;
        }
    }
    public float NotInfected;
    public int Money;

    public Culture MyCulture;
    public Region Region;

    public City(float population, Region region, Culture myCulture)
    {
        TotalPopulation += (int)(population / Person);
        NotInfected = population;
        Symptomatic = 0;
        Money = 0;
        MyCulture = myCulture;
        Region = region;
        Asymptomatic = new Queue<float>();
    }

    public void UpdatePerDay(Virus virus)
    {
        float deaths = Symptomatic * 0.4f;//virus.Lethality(Region);
        Symptomatic -= deaths;
        TotalPopulation -= (int)(deaths / Person);
        float newInfected = NotInfected * Infected * 1.05f;//virus.InfectRate(Region);
        newInfected = newInfected <= NotInfected ? newInfected : NotInfected;

        Asymptomatic.Enqueue(newInfected);
        NotInfected -= newInfected;

        if (Asymptomatic.Count > virus.SerialRangeRnd())
        {
            float value = Asymptomatic.Dequeue();
            if (Symptomatic == 0 && value > 0)
                Region.OnRegionInfected();
            Symptomatic += value;
        }
    }

    public void UpdatePerWeek()
    {
        NotInfected = Population * 1.0005f;
    }

    public void ReceiveInfected(float infected)
    {
        float asymptomatic = 0;
        foreach (float a in Asymptomatic)
            asymptomatic += a;
        Symptomatic += infected * Symptomatic / Infected;
        infected *= asymptomatic / Infected;
        Queue<float> newAsymptomatic = new Queue<float>();
        foreach (float a in Asymptomatic)
            newAsymptomatic.Enqueue(infected * a / asymptomatic);
        Asymptomatic = newAsymptomatic;
    }

    public static (float notInfected, float Infected) MigrationPerDay
        (City from, City to)
    {
        float deltaPopulationDensity = from.Population /
            (from.Population + to.Population + 0.0000001f);
        float deltaInfected = from.Symptomatic /
            (from.Symptomatic + to.Symptomatic + 0.0000001f);
        float deltaMoney = to.Money / (from.Money + to.Money + 0.0000001f);
        float migration =
            deltaMoney * 0.5f +
            deltaInfected * 0.3f +
            deltaPopulationDensity * 0.2f;

        if (migration > 0f)
            return (migration * from.NotInfected * 0.2f,
                migration * from.Infected * 0.2f);
        return (0, 0);
    }
}