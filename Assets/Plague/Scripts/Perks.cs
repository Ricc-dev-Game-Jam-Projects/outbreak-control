public abstract class Perks
{
    public string Name { get; private set; }
    public string Description { get; private set; }


    public Perks(string nome, string description)
    {
        this.Name = nome;
        this.Description = description;
    }


    public abstract void Skill();


}