using System.Collections;
using System.Collections.Generic;

public class Virus 
{
    public string Name { get; private set; } 
    public float ChanceOfContamination { get; private set; }
    public float SpreadingSpeed { get; private set; }

    private List<Symptoms> MySymptoms;// Sintomas do virus
    private List<Transmission> MyTransmissions; //meios de Transmissao do virus


    public Virus(string name, ETransmission way, float coc)
    {
        Name = name;
        ChanceOfContamination = coc;
        MySymptoms = new List<Symptoms>();
        MyTransmissions = new List<Transmission>();
    }

    public override string ToString()
    {
        string transmissions = "";
        foreach(Transmission t in MyTransmissions)
        {
            transmissions += " " + t.Name;
        }

        string symptoms = "";
        foreach(Symptoms s in MySymptoms)
        {
            symptoms += " " + s.Name;
        }


        return string.Format("The virus {0}, transmitted through {1},  with the symptoms: {2}, Iminent Outbreak", Name, 
          transmissions, symptoms);
    }

    public void Infect(/*Population p */)
    {

    }

    public void UpdateSpreadingSpeed(float rating)
    {
        SpreadingSpeed += rating;
    }



}
