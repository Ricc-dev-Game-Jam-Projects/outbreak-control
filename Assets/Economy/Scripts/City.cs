using System;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public const int MaxPopulation = 1000;
    public float PopulationDensity {
        get { return PopulationSize / MaxPopulation; }
        set { PopulationSize = (int)(value * MaxPopulation); }
    }
    public int PopulationSize;
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
    public int Money;

    public Culture MyCulture;
    public Region Region;

    public City(Region region, Culture myCulture)
    {
        PopulationSize = 0;
        Symptomatic = 0;
        Money = 0;
        MyCulture = myCulture;
        Region = region;
        Asymptomatic = new Queue<float>();
    }

    public void UpdatePerDay(Virus virus)
    {
        int deaths = (int)(PopulationSize * Symptomatic * virus.Lethality(Region));
        PopulationSize = (int)Math.Pow(PopulationSize - deaths, 1.001);
        Asymptomatic.Enqueue(virus.InfectRate(Region) * Infected);
        if (Asymptomatic.Count > virus.SerialRangeRnd())
            Symptomatic += Asymptomatic.Dequeue();
    }

    public static void MigrationPerDay(City from, City to)
    {
        float deltaPopulationDensity = from.PopulationDensity /
            (from.PopulationDensity + to.PopulationDensity + 0.00001f) - 0.5f;
        float deltaInfected = from.Symptomatic /
            (from.Symptomatic + to.Symptomatic + 0.00001f) - 0.5f;
        float deltaMoney = to.Money / (from.Money + to.Money + 0.00001f) - 0.5f;
        float migration =
            deltaMoney * 0.5f +
            deltaInfected * 0.3f +
            deltaPopulationDensity * 0.2f;

        if(migration > 0)
        {
            int totalMigration = (int)(migration * from.PopulationSize * 0.5);
            from.PopulationSize -= totalMigration;
            to.PopulationSize += totalMigration;
        }
    }
}