using System;

public class Transmission : Perks
{
    public float Infected { get; private set; } //numero de pessoas infectadas por unidade de tempo
    public ETransmission TransmissionType { get; private set; }
    private float EvolveNum { get; set; }

    public Transmission(string name, string description, ETransmission e) : base(name, description)
    {
        TransmissionType = e;
        EvolveNum = 0.3f;
        Infected = 0;
    }

    public override void EvolvePerk()
    {
        if(PerkLevel < 4)
        {
            PerkLevel++;
            ContagionRate += EvolveNum;
            EvolveNum -= EvolveNum - 0.06f; //evoluir o perk nessa velocidade
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
}

