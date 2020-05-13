using System;
using UnityEngine;

public class City
{
    public const int MaxPopulation = 1000;
    public float PopulationDensity {
        get { return PopulationSize / MaxPopulation; }
        set { PopulationSize = (int)(value * MaxPopulation); }
    }
    public int PopulationSize;
    public float Infected;
    public int Money;

    public Culture MyCulture;

    public City(int populationSize, float infected, int money, Culture myCulture)
    {
        PopulationSize = populationSize;
        Infected = infected;
        Money = money;
        MyCulture = myCulture;
    }

    public void UpdatePerDay(Virus virus)
    {
        PopulationSize *= (int)(1 - Infected * 0.5f);
        Infected *= 1 + 0.2f;
    }

    public static void MigrationPerDay(City from, City to)
    {
        float ΔpopulationDensity = to.PopulationDensity - from.PopulationDensity;
        float Δinfected = to.Infected - from.Infected;
        int Δmoney = to.Money - from.Money;
        int totalMoney = to.Money + from.Money;
        float migration =
            Δmoney / totalMoney * 0.5f +
            (1 - Δinfected) * 0.3f +
            (1 - ΔpopulationDensity) * 0.2f;
        Debug.Log(migration);
    }
}