using System;
using System.Collections.Generic;

public  class Culture
{
    public string Description { get; private set; } //descricao da cultura
    public Dictionary<ETransmission, int> DiseaseWeakness { get;private set; } // Tipos de transmissoes sucetiveis
    public Dictionary<ETransmission, int> DiseaseStrength { get; private set; } //tipos de transmissoes menos sucetiveis

    public Culture(string description)
    {
        DiseaseStrength = new Dictionary<ETransmission, int>();
        DiseaseWeakness = new Dictionary<ETransmission, int>();
        Description = description;
    }
}

