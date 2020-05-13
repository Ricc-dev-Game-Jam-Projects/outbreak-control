using System;

public class City
{
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
        //PopulationSiz0.5f
    }
}