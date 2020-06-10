using System;
using System.Collections.Generic;
using UnityEngine;
using jam;

public class City
{
  public static float TotalPopulation = 0;
  public Population population;
  public int Money;
  public Economy economy;

  public Culture MyCulture;
  public Region Region;

  public City(float population, Region region, Culture myCulture)
  {
    TotalPopulation += population;
    this.population = new Population(population, new Queue(), new Queue(), 0);
    Money = 500;
    MyCulture = myCulture;
    Region = region;
    economy = new Economy((int)(population / Population.p), Money);
  }

  public void UpdatePerDay(Virus virus)
  {
    population.Update(virus, Region);
  }

  public void UpdatePerWeek()
  {
    float baby = population.Total * 0.0005f;
    population.Susceptible += baby;
    TotalPopulation += baby;
  }

  public static float MigrationIntensity(City from, City to)
  {
    float d = 0.0000001f;
    float deltaDensity = from.population.Density /
      (from.population.Density + to.population.Density + d);
    float deltaInfectious = from.population.SymptomaticDensity /
      (from.population.SymptomaticDensity + to.population.SymptomaticDensity + d);
    float deltaMoney = to.economy.CurrentMoney /
      (from.economy.CurrentMoney + to.economy.CurrentMoney + d);

    return deltaInfectious - 0.5f;
    //return (deltaMoney + deltaInfectious + deltaDensity) / 3 - 0.5f;
  }
}