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
        PopulationSize *= (int)(1 - Infected * 0.5f);
        Infected *= 1 + 0.2f;
    }

    public static void MigrationPerDay(City city1, City city2)
    {
        
    }
}