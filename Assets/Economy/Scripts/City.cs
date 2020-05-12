using System;

public class City
{
    public float PopulationSize { get; set; } // NUmero de cidadoes da cidade
    public bool IsInfected { get; private set; } // Se a cidade ja esta infectada

    public Culture MyCulture { get; private set; }

}