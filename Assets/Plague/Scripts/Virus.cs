using System.Collections;
using System.Collections.Generic;

public class Virus 
{
    public string Name { get; private set; }
    public ETransmission Way { get; private set; }    
    public float ChanceOfContamination { get; private set; }
    //public delegate void Lethality();
    public Virosis Virosis { get; private set; }

    public Virus(string name, ETransmission way, float coc)
    {
        Name = name;
        Way = way;
        ChanceOfContamination = coc;
        Virosis = new Virosis(name + "eesy");
    }

    public override string ToString()
    {
        string transmissions = "";
        if(Way.HasFlag(ETransmission.Airborne))
        {
            transmissions += " Airborne";
        }
        if (Way.HasFlag(ETransmission.Bloodborne))
        {
            transmissions += " Bloodborne";
        }
        if (Way.HasFlag(ETransmission.Sexually))
        {
            transmissions += " Sexually";
        }
        if (Way.HasFlag(ETransmission.Touching))
        {
            transmissions += " Touching";
        }
        if (Way.HasFlag(ETransmission.Zoonosis))
        {
            transmissions += " Zoonosis";
        }

        return string.Format("The virus {0}, transmitted through {1},  iminent Outbreak, ", Name, 
          transmissions);
    }
}
