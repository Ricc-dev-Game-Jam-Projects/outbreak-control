public abstract class Perks
{
    public string Name { get; private set; }
    public string Description { get; protected set; }
    public float ContagionRate { get; protected set; }
    public int PerkLevel { get; protected set; }

    public Perks(string nome, string description)
    {
        this.Name = nome;
        this.Description = description;
        this.PerkLevel = 1;
        ContagionRate = 0.2f;
    }


    public abstract void Skill();

    public abstract void EvolvePerk();

    public abstract void SetContagionRate();


}