using System;

public class Transmission : Perk
{
    public ETransmission TransmissionType { get; private set; }
    public float Infected { get; private set; } // numero de pessoas infectadas por unidade de tempo
    
    private float EvolveNum { get; set; }

    public Transmission(string name, string description, int perkLevel, float infected, ETransmission transmission, float contagionRate) : 
                    base(name, description, perkLevel, contagionRate)
    {
        TransmissionType = transmission;
        EvolveNum = 0.3f;
        Infected = 0;
        Infected = infected;
    }

    public override void EvolvePerk(Perk PerkEvolved)
    {
        base.EvolvePerk(PerkEvolved);
        if (PerkLevel < 4)
        {
            PerkLevel++;
            ContagionRate += EvolveNum;
            EvolveNum -= 0.06f; //evoluir o perk nessa velocidade
        }
    }

    public override void SetContagionRate()
    {
        throw new NotImplementedException();
    }

    public override void Skill()
    {
        Infected += Infected * PerkLevel/10; // incrementa o numero de infectados somando ele com o numero atual * level do perk/ 10
    }

    public Transmission Clone()
    {
        return (Transmission)MemberwiseClone();
    }
}

