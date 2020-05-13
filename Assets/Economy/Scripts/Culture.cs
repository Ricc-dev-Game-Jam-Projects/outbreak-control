using System;
using System.Collections.Generic;

public  class Culture
{
    public string Description { get; private set; } //descricao da cultura
    public Dictionary<ETransmission, int> DiseaseWeakness { get;private set; } // Tipos de transmissoes sucetiveis
    public Dictionary<ETransmission, int> DiseaseStrength { get; private set; } //tipos de transmissoes menos sucetiveis
    public float Warmness { get; private set; } // O quao calorosa eh a cultura, o que influencia a infeccao da doenca

    public Culture(string description)
    {
        Random temp = new Random();
        DiseaseStrength = new Dictionary<ETransmission, int>();
        DiseaseWeakness = new Dictionary<ETransmission, int>();
        Description = description;
        Warmness = temp.Next(0, 1); //Gerando o valor de warmness dentro de um range entre 0 e 1
    }

}

