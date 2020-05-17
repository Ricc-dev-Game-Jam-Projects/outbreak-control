using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus
{
    public string Name { get; private set; }
    public float Spreading; // Média de de pessoas contagiadas por 1 pessoa infectada (esse valor pode abaixar com as medidas)
    public float SerialRange; // A cada quantos dias são manifestados os sintomas, contabilizando mais infectados
    public float FatalityCase; // Qual a média de fatalidade entre os infectados
    public float InfectionTime;

    public List<Symptom> MySymptoms { get; private set; } // Sintomas do virus
    public List<Transmission> MyTransmissions { get; private set; } //meios de Transmissao do virus
    
    public Dictionary<string, Perk[]> Perks;

    public int PerkNumber = 2;

    public Virus(string name, float spr)
    {
        Name = name;
        Spreading = spr;
        MySymptoms = new List<Symptom>();
        MyTransmissions = new List<Transmission>();
        Perks = new Dictionary<string, Perk[]>
        {
            { "Symptoms", MySymptoms.ToArray() as Perk[] },
            { "Transmission", MyTransmissions.ToArray() as Perk[] }
        };
        
        Spreading = Random.Range(0.7f, 1.7f);
        SerialRange = Random.Range(1f, 5f);
        FatalityCase = Random.Range(0.0005f, 0.03f);
        InfectionTime = Random.Range(7f, 14f);


        Debug.Log("Virus Spreading " + Spreading);
        Debug.Log("Virus Serial Range " + SerialRange);
        Debug.Log("Virus Fatality Cases " + FatalityCase);        
    }

    public override string ToString()
    {
        string transmissions = "";
        foreach (Transmission t in MyTransmissions)
        {
            if (t == null) continue;
            transmissions += " " + t.Name;
        }

        string symptoms = "";
        foreach (Symptom s in MySymptoms)
        {
            if (s == null) continue;
            symptoms += " " + s.Name;
        }


        return string.Format("Virus: {0}, " +
            "Ways of transmission: {1},  Symptoms: {2}" +
            " Iminent Outbreak", Name,
          transmissions, symptoms);
    }

    public float InfectRate(Region region)
    {
        // float de 0 a 1
        float intensity = 0f;
        Culture culture = region.city.MyCulture;

        foreach(Transmission t in MyTransmissions)
        {
            if (t == null) continue;
            if (culture.TransmissionWeakness.ContainsKey(t.TransmissionType) && 
                culture.TransmissionWeakness[t.TransmissionType] <= t.PerkLevel)
            {
                intensity += t.ContagionRate;
            }
        }
        float sprd = Spreading + (intensity * Spreading);
        return sprd <= 0 ? 0 : sprd;
    }

    public float Lethality(Region region)
    {
        float intensity = 0f;
        Culture culture = region.city.MyCulture;

        foreach (Symptom s in MySymptoms)
        {
            if (s == null) continue;
            if (culture.SystemWeakness.ContainsKey(s.Systems) && culture.SystemWeakness[s.Systems] <= s.PerkLevel)
            {
                intensity += s.LethalityRate/100f;
            }
        }

        float leta = FatalityCase + (intensity * FatalityCase);
        return leta <= 0 ? 0 : leta;
    }

    public int SerialRangeRnd()
    {
        return (int) Mathf.Clamp(Random.Range(SerialRange - 1, SerialRange + 1), 0, 15);
    }

    public static float CalculateSpreading(Virus v)
    {
        float TotalTransmission = v.Spreading;
        foreach(Transmission t in v.MyTransmissions)
        {
            TotalTransmission += t.ContagionRate;
        }
            
        return TotalTransmission;
    }
 
    public float GetSpreadingOnCulture(Culture c, float PopulationDensity)
    {
        float Spread= Virus.CalculateSpreading(this);
        Spread += c.Warmness;

        return Spread;
    }

    public void AddSymptom(Symptom symptoms)
    {
        MySymptoms.Add(symptoms);
    }

    public void AddTransmission(Transmission transmission)
    {
        MyTransmissions.Add(transmission);
    }
}




















//string a = "nothing"