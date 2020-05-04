using System;
using System.Collections.Generic;

public class Virosis
{
    public string Name { get; private set; }
    private List<Symptoms> MySymptoms;

    public Virosis(string name)
    {
        Name = name;
        MySymptoms = new List<Symptoms>();
    }


    public override string ToString()
    {
        string symptoms = "";
        foreach(Symptoms symptom in MySymptoms)
        {
            symptoms += symptom.ToString();
        }
        return symptoms;
    }

    public void AddSymptoms(Symptoms e)
    {
        MySymptoms.Add(e);
    }

    public Symptoms[] GetSymptoms()
    {
        return MySymptoms.ToArray();
    }



}