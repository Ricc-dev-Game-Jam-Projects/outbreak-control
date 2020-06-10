using UnityEngine.Events;

public abstract class Perk
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public string ImagePath;
    public int PerkLevel { get; protected set; }
    public UnityAction Skill;

    public Perk(string nome, string description, int perkLevel)
    {
        this.Name = nome;
        this.Description = description;
        PerkLevel = perkLevel;
    }

    public virtual void EvolvePerk(Perk perkEvolved)
    {
        Name = perkEvolved.Name;
        Description = perkEvolved.Description;
        PerkLevel = perkEvolved.PerkLevel;
    }

    public abstract void SetContagionRate();

}