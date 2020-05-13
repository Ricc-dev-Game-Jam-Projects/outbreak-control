using System.Collections;
using System.Collections.Generic;

public class Virus
{
    public string Name { get; private set; }
    public float ChanceOfContamination { get; private set; }
    public float SpreadingSpeed { get; private set; }

    public List<Symptom> MySymptoms { get; private set; } // Sintomas do virus
    public List<Transmission> MyTransmissions { get; private set; } //meios de Transmissao do virus
    
    public Dictionary<string, Perk[]> Perks;

    public int PerkNumber = 2;

    public Virus(string name, float coc)
    {
        Name = name;
        ChanceOfContamination = coc;
        MySymptoms = new List<Symptom>();
        MyTransmissions = new List<Transmission>();
        Perks = new Dictionary<string, Perk[]>
        {
            { "Symptoms", MySymptoms.ToArray() as Perk[] },
            { "Transmission", MyTransmissions.ToArray() as Perk[] }
        };
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


        return string.Format("The virus {0}, transmitted through {1},  with the symptoms: {2}, Iminent Outbreak", Name,
          transmissions, symptoms);
    }

    public float InfectRate(Region region)
    {
        // float de 0 a 1
        float intensity = 0f;
        Culture culture = region.city.MyCulture;

        foreach(Transmission t in MyTransmissions)
        {
            if (culture.TransmissionWeakness.ContainsKey(t.TransmissionType) && 
                culture.TransmissionWeakness[t.TransmissionType] <= t.PerkLevel)
            {
                intensity += t.ContagionRate;
            }
        }

        return intensity;
    }

    public float Lethality(Region region)
    {
        float intensity = 0f;
        Culture culture = region.city.MyCulture;

        foreach (Symptom s in MySymptoms)
        {
            if (culture.SystemWeakness.ContainsKey(s.Systems) && culture.SystemWeakness[s.Systems] <= s.PerkLevel)
            {
                intensity += s.LethalityRate;
            }
        }

        return intensity;
    }

    public void UpdateSpreadingSpeed(float rating)
    {
        SpreadingSpeed += rating;
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
