using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Population
{
  public static float p = 1e-3f;
  public static float Max = 1;

  public float Immune;
  public Queue Symptomatic;
  public Queue Asymptomatic;
  public float Susceptible;
  public float Total {
    get => Susceptible + Infectious + Immune;
  }
  public float Infectious {
    get => Symptomatic.ToFloat() + Asymptomatic.ToFloat();
  }
  public float Density {
    get => Total / Max;
  }
  public float SymptomaticDensity {
    get => Symptomatic.ToFloat() / Total;
  }

  public Population(
      float susceptible, Queue asymptomatic,
      Queue symptomatic, float immune)
  {
    Susceptible = susceptible;
    Asymptomatic = asymptomatic;
    Symptomatic = symptomatic;
    Immune = immune;
  }

  public void Update(Virus virus, Region region)
  {
    // susceptible ~> asymptomatic
    float newInfectious = Susceptible * Infectious * virus.InfectRate(region);
    newInfectious = newInfectious < Susceptible ? newInfectious : Susceptible;

    Susceptible -= newInfectious;
    Asymptomatic.Enqueue(newInfectious);

    // asymptomatic ~> symptomatic
    if (Asymptomatic.Count >= virus.SerialRangeRnd())
    {
      float newSymptomatic = Asymptomatic.Dequeue();

      // primeiro caso de infecção detectado na região
      if (Symptomatic.ToFloat() <= p && newSymptomatic >= p)
        region.OnRegionInfected();
      
      Symptomatic.Enqueue(newSymptomatic);
    }

    // symptomatic ~> immune | dead
    if (Symptomatic.Count >= virus.GetCureTime())//dias doente
    {
      float recovered = Symptomatic.Dequeue();
      float lethality = virus.Lethality(region);

      Immune += recovered * (1 - lethality);
      City.TotalPopulation -= recovered * lethality;
    }

    //if(region.X == 20 && region.Y == 15)
    //{
    //  string asympText = "", sympText = "";
    //  foreach (var a in Asymptomatic) asympText += ((int)(a / p)) + "; ";
    //  foreach (var s in Symptomatic) sympText += ((int)(s / p)) + "; ";
    //  Debug.Log(
    //    "Susceptible: " + ((int)(Susceptible / p)) +
    //    "\nAsymptomatic: " + asympText +
    //    "\nSymptomatic: " + sympText +
    //    "\nImmune: " + ((int)(Immune / p)) +
    //    "\nNew Infectious: " + ((int)(newInfectious / p)));
    //}
  }

  public static Population operator *(Population population, float percent)
  {
    float susceptible = population.Susceptible * percent;
    Queue asymptomatic = population.Asymptomatic * percent;
    Queue symptomatic = population.Symptomatic * percent;
    float immune = population.Immune * percent;

    return new Population(susceptible, asymptomatic, symptomatic, immune);
  }

  public static Population operator /(Population population, float denominator)
  {
    float susceptible = population.Susceptible * denominator;
    Queue asymptomatic = population.Asymptomatic * denominator;
    Queue symptomatic = population.Symptomatic * denominator;
    float immune = population.Immune * denominator;

    return new Population(susceptible, asymptomatic, symptomatic, immune);
  }

  public static Population operator +(Population p1, Population p2)
  {
    float susceptible = p1.Susceptible + p2.Susceptible;
    Queue asymptomatic = p1.Asymptomatic + p2.Asymptomatic;
    Queue symptomatic = p1.Symptomatic + p2.Symptomatic;
    float immune = p1.Immune + p2.Immune;

    return new Population(susceptible, asymptomatic, symptomatic, immune);
  }

  public static Population operator -(Population p1, Population p2)
  {
    float susceptible = p1.Susceptible - p2.Susceptible;
    if (susceptible < 0)
    {
      susceptible = 0;
      throw new Exception("susceptible almost get a negative value");
    }
    Queue asymptomatic = p1.Asymptomatic - p2.Asymptomatic;
    Queue symptomatic = p1.Symptomatic - p2.Symptomatic;
    float immune = p1.Immune - p2.Immune;
    if (immune < 0)
    {
      immune = 0;
      throw new Exception("immune almost get a negative value");
    }

    return new Population(susceptible, asymptomatic, symptomatic, immune);
  }
}
