using UnityEngine.Events;

public abstract class Perk
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public string ImagePath;
    public float ContagionRate { get; protected set; }
    public int PerkLevel { get; protected set; }
    public UnityAction Skill;

    public Perk(string nome, string description, int perkLevel, float contagionRate = 0.2f)
    {
        this.Name = nome;
        this.Description = description;
        ContagionRate = contagionRate;
        PerkLevel = perkLevel;
    }

    public virtual void EvolvePerk(Perk perkEvolved)
    {
        Name = perkEvolved.Name;
        Description = perkEvolved.Description;
        PerkLevel = perkEvolved.PerkLevel;
        ContagionRate = perkEvolved.ContagionRate;
    }

    public abstract void SetContagionRate();

}