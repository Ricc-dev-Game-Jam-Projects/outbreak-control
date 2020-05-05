using System;
using System.Collections.Generic;

public class Symptoms: Perks
{
    
    public Dictionary<ESystems,short> Systems { get; private set; } //sistemas que o virus atinge
    public float LethalityRate { get; private set; }// chance de letalidade

    public float Deceased { get; private set; } // numero de pessoas mortas por unidade de tempo


    public Symptoms(string name, string descripton) : base(name, descripton)
    {
        Systems = new Dictionary<ESystems, short>();
    }

    public override string ToString() //override do metodo ToString();
    {
        string temp = "";
        foreach(KeyValuePair<ESystems,short> s in Systems)
        {
            temp += Symptoms.SystemToString(s.Key) + " with value of " + s.Value + "\n";
        }
        return temp;
    }

    public static string SystemToString(ESystems system) //Metodo que transforma o sistema em string
    {

       switch(system)
        {
            case ESystems.Digestive: return "Digestive";
            case ESystems.Immunologic: return "Immunologic";
            case ESystems.Neurologic: return "Neurologic";
            case ESystems.Respiratory: return "Respiratory";
            default: return "";
        }

    }

    public override void Skill()
    {
        
    }

    public override void SetContagionRate()
    {
        throw new NotImplementedException();
    }

    public override void EvolvePerk()
    {
        throw new NotImplementedException();
    }
}
