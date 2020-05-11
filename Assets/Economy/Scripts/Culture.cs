using System;
using System.Collections.Generic;

public  class Culture
{
    public string Description { get; private set; } //descricao da cultura
    public List<ETransmission> DiseaseWeakness { get;private set; } // Tipos de transmissoes sucetiveis
    public List<ETransmission> DiseaseStrength { get; private set; } //tipos de transmissoes menos sucetiveis

    public Culture(string description)
    {
        DiseaseStrength = new List<ETransmission>();
        DiseaseWeakness = new List<ETransmission>();
        Description = description;
    }
}

