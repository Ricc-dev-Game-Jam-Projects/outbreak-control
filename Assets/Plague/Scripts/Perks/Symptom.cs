using System;
using System.Collections.Generic;

public class Symptom : Perk
{
    public ESystems Systems { get; private set; } // sistemas que o virus atinge
    public float LethalityRate { get; private set; } // chance de letalidade

    public Symptom(string name, string descripton, int perkLevel, float lethalityRate, ESystems system, float contagionRate = 0.2f) : 
                base(name, descripton, perkLevel, contagionRate)
    {
        LethalityRate = lethalityRate;
        Systems = system;
    }

    //override do metodo ToString();
    public override string ToString() 
    {
        return SystemToString(Systems) + " with value of " + LethalityRate + "\n";
    }

    //Metodo que transforma o sistema em string
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

    public override void SetContagionRate()
    {
        
    }

    public override void EvolvePerk(Perk PerkEvolved)
    {
        base.EvolvePerk(PerkEvolved);
        Symptom sym = PerkEvolved as Symptom;

        LethalityRate = sym.LethalityRate;
    }

    public Symptom Clone()
    {
        return (Symptom)MemberwiseClone();
    }
}
