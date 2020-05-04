using System;
using System.Collections.Generic;

public class Symptoms: Perks
{
    
    public Dictionary<ESystems,short> System { get; private set; }

    public Symptoms(string name, string descripton) : base(name, descripton)
    {
        System = new Dictionary<ESystems, short>();
    }

    public override string ToString()
    {
        string temp = "";
        foreach(KeyValuePair<ESystems,short> s in System)
        {
            temp += Symptoms.SystemToString(s.Key) + " with value of " + s.Value + "\n";
        }
        return temp;
    }

    public static string SystemToString(ESystems system)
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
}
