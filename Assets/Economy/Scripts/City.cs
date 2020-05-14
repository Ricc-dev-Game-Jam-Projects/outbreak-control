using System;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public const int MaxPopulation = 1000;
    public float RelPopulation {
        get { return (float)AbsPopulation / MaxPopulation; }
        set { AbsPopulation = (int)(value * MaxPopulation); }
    }
    public int AbsPopulation;
    public float RelSymptomatic {
        get { return (float)AbsSymptomatic / AbsPopulation; }
        set { AbsSymptomatic = (int)(value * AbsPopulation); }
    }
    public int AbsSymptomatic;
    public Queue<int> Asymptomatic;
    public float RelInfected {
        get {
            float infected = RelSymptomatic;
            foreach (int a in Asymptomatic)
                infected += (float)a / AbsPopulation;
            return infected;
        }
    }
    public int AbsInfected {
        get {
            int infected = AbsSymptomatic;
            foreach (int a in Asymptomatic)
                infected += a;
            return infected;
        }
    }
    public int Money;

    public Culture MyCulture;
    public Region Region;

    public City(Region region, Culture myCulture)
    {
        AbsPopulation = 0;
        RelSymptomatic = 0;
        Money = 0;
        MyCulture = myCulture;
        Region = region;
        Asymptomatic = new Queue<int>();
    }

    public void UpdatePerDay(Virus virus)
    {
        float deaths = RelPopulation * RelSymptomatic * virus.Lethality(Region);
        RelPopulation -= deaths;
        Asymptomatic.Enqueue((int)virus.InfectRate(Region) * AbsInfected);
        if (Asymptomatic.Count > virus.SerialRangeRnd())
            RelSymptomatic += Asymptomatic.Dequeue();
    }

    public void UpdatePerWeek()
    {
        RelPopulation *= 1.005f;
    }

    public static int MigrationPerDay(City from, City to)
    {
        float deltaPopulationDensity = from.RelPopulation /
            (from.RelPopulation + to.RelPopulation + 0.00001f);
        float deltaInfected = from.RelSymptomatic /
            (from.RelSymptomatic + to.RelSymptomatic + 0.00001f);
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