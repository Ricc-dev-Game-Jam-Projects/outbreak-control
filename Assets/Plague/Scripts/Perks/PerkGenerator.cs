using System.Collections.Generic;
using UnityEngine;

public class PerkGenerator
{
    public Symptom[][] SymptomsPerks;
    public Transmission[][] TransmissionPerks;

    public int SymptomsSize { get; private set; } = 16;
    public int SymptomsMaxLevel { get; private set; } = 4;
    public int TransmissionSize { get; private set; } = 16;
    public int TransmissionMaxLevel { get; private set; } = 4;

    public int MaxPoints {
        get {
            return (int) ((SymptomsSize + TransmissionSize) * 0.625f);
        }
    }

    public int UsedPoints { get; set; } = 0;

    public PerkGenerator()
    {
        GenerateBasicPerks();
    }

    public bool CanUsePoints()
    {
        return UsedPoints < MaxPoints;
    }

    public void GeneratePerks(out Symptom[] symptoms, out Transmission[] transmissions)
    {
        symptoms = new Symptom[SymptomsMaxLevel];
        transmissions = new Transmission[TransmissionMaxLevel];

        int pog = 500;

        while (CanUsePoints() && pog != 0)
        {
            int typ = Random.Range(0, 2);

            int subtype;

            switch (typ)
            {
                case 0:
                    subtype = Random.Range(0, SymptomsSize/SymptomsMaxLevel);
                    if (symptoms[subtype] == null)
                    {
                        symptoms[subtype] = SymptomsPerks[subtype][0].Clone();
                        UsedPoints++;
                        Debug.Log("Added " + symptoms[subtype].Name + " Points used: " + UsedPoints);
                    } else
                    {
                        if(symptoms[subtype].PerkLevel < SymptomsMaxLevel)
                        {
                            symptoms[subtype].EvolvePerk(SymptomsPerks[subtype][symptoms[subtype].PerkLevel]);
                            UsedPoints++;
                            Debug.Log("Added " + symptoms[subtype].Name + " Points used: " + UsedPoints);
                        }
                    }
                    break;
                case 1:
                    subtype = Random.Range(0, TransmissionSize / TransmissionMaxLevel);
                    if (transmissions[subtype] == null)
                    {
                        transmissions[subtype] = TransmissionPerks[subtype][0].Clone();
                        UsedPoints++;
                        Debug.Log("Added " + transmissions[subtype].Name + " Points used: " + UsedPoints);
                    }
                    else
                    {
                        if (transmissions[subtype].PerkLevel < TransmissionMaxLevel)
                        {
                            transmissions[subtype].EvolvePerk(TransmissionPerks[subtype][transmissions[subtype].PerkLevel]);
                            UsedPoints++;
                            Debug.Log("Added " + transmissions[subtype].Name + " Points used: " + UsedPoints);
                        }
                    }
                    break;
                default:
                    break;
            }

            pog--;
        }
    }

    private void GenerateBasicPerks()
    {
        SymptomsPerks = new Symptom[SymptomsMaxLevel][];
        TransmissionPerks = new Transmission[TransmissionMaxLevel][];

        for(int i = 0; i < SymptomsMaxLevel; i++)
        {
            SymptomsPerks[i] = new Symptom[SymptomsMaxLevel];
            TransmissionPerks[i] = new Transmission[SymptomsMaxLevel];
        }

        string PerkPath = "images/perks/";

        // Immunologic

        SymptomsPerks[0][0] = new Symptom(name: "Immunologic I",
                                            descripton: "Affects slightly the immunologic system",
                                            perkLevel: 1,
                                            lethalityRate: 0.2f,
                                            system: ESystems.Immunologic,
                                            contagionRate: 0.4f);

        SymptomsPerks[0][1] = new Symptom(name: "Immunologic II",
                                            descripton: "Affects the immunologic system",
                                            perkLevel: 2,
                                            lethalityRate: 0.4f,
                                            system: ESystems.Immunologic,
                                            contagionRate: 0.3f);

        SymptomsPerks[0][2] = new Symptom(name: "Immunologic III",
                                            descripton: "Affects moderately the immunologic system",
                                            perkLevel: 3,
                                            lethalityRate: 0.6f,
                                            system: ESystems.Immunologic,
                                            contagionRate: 0.2f);

        SymptomsPerks[0][3] = new Symptom(name: "Immunologic IV",
                                            descripton: "Affects intensely the immunologic system",
                                            perkLevel: 4,
                                            lethalityRate: 0.7f,
                                            system: ESystems.Immunologic,
                                            contagionRate: 0.15f);

        // Respiratory

        SymptomsPerks[1][0] = new Symptom(name: "Respiratory I",
                                            descripton: "Affects slightly the respiratory system",
                                            perkLevel: 1,
                                            lethalityRate: 0.2f,
                                            system: ESystems.Respiratory,
                                            contagionRate: 0.3f);

        SymptomsPerks[1][1] = new Symptom(name: "Respiratory II",
                                            descripton: "Affects the respiratory system",
                                            perkLevel: 2,
                                            lethalityRate: 0.25f,
                                            system: ESystems.Respiratory,
                                            contagionRate: 0.45f);

        SymptomsPerks[1][2] = new Symptom(name: "Respiratory III",
                                            descripton: "Affects moderately the respiratory system",
                                            perkLevel: 3,
                                            lethalityRate: 0.35f,
                                            system: ESystems.Respiratory,
                                            contagionRate: 0.60f);

        SymptomsPerks[1][3] = new Symptom(name: "Respiratory IV",
                                            descripton: "Affects intensely the respiratory system",
                                            perkLevel: 4,
                                            lethalityRate: 0.45f,
                                            system: ESystems.Respiratory,
                                            contagionRate: 0.75f);

        // Neurologic

        SymptomsPerks[2][0] = new Symptom(name: "Neurologic I",
                                            descripton: "Affects slightly the neurologic system",
                                            perkLevel: 1,
                                            lethalityRate: 0.2f,
                                            system: ESystems.Neurologic,
                                            contagionRate: 0.3f);

        SymptomsPerks[2][1] = new Symptom(name: "Neurologic II",
                                            descripton: "Affects the neurologic system",
                                            perkLevel: 2,
                                            lethalityRate: 0.25f,
                                            system: ESystems.Neurologic,
                                            contagionRate: 0.45f);

        SymptomsPerks[2][2] = new Symptom(name: "Neurologic III",
                                            descripton: "Affects moderately the neurologic system",
                                            perkLevel: 3,
                                            lethalityRate: 0.35f,
                                            system: ESystems.Neurologic,
                                            contagionRate: 0.60f);

        SymptomsPerks[2][3] = new Symptom(name: "Neurologic IV",
                                            descripton: "Affects intensely the neurologic system",
                                            perkLevel: 4,
                                            lethalityRate: 0.45f,
                                            system: ESystems.Neurologic,
                                            contagionRate: 0.75f);

        // Digestive

        SymptomsPerks[3][0] = new Symptom(name: "Digestive I",
                                            descripton: "Affects slightly the digestive system",
                                            perkLevel: 1,
                                            lethalityRate: 0.2f,
                                            system: ESystems.Digestive,
                                            contagionRate: 0.3f);

        SymptomsPerks[3][1] = new Symptom(name: "Digestive II",
                                            descripton: "Affects the digestive system",
                                            perkLevel: 2,
                                            lethalityRate: 0.25f,
                                            system: ESystems.Digestive,
                                            contagionRate: 0.45f);

        SymptomsPerks[3][2] = new Symptom(name: "Digestive III",
                                            descripton: "Affects moderately the digestive system",
                                            perkLevel: 3,
                                            lethalityRate: 0.35f,
                                            system: ESystems.Digestive,
                                            contagionRate: 0.60f);

        SymptomsPerks[3][3] = new Symptom(name: "Digestive IV",
                                            descripton: "Affects intensely the digestive system",
                                            perkLevel: 4,
                                            lethalityRate: 0.45f,
                                            system: ESystems.Digestive,
                                            contagionRate: 0.75f);

        int cont = 0;
        foreach (Symptom[] s in SymptomsPerks)
        {
            foreach(Symptom ss in s)
            {
                if (cont == 0)
                {
                    ss.ImagePath = PerkPath + "airborne";
                }
                else if (cont == 1)
                {
                    ss.ImagePath = PerkPath + "water";
                }
                else if (cont == 2)
                {
                    ss.ImagePath = PerkPath + "food";
                }
                else if (cont == 3)
                {
                    ss.ImagePath = PerkPath + "zoonosis";
                }
            }
            cont++;
        }

        // Air

        TransmissionPerks[0][0] = new Transmission(name: "Airborne I",
                                            description: "The contagious can be transmitted by airborne",
                                            perkLevel: 1,
                                            infected: 1f,
                                            transmission: ETransmission.Airborne,
                                            contagionRate: 0.45f);

        TransmissionPerks[0][1] = new Transmission(name: "Airborne II",
                                            description: "The contagious can be transmitted by airborne",
                                            perkLevel: 2,
                                            infected: 1f,
                                            transmission: ETransmission.Airborne,
                                            contagionRate: 0.45f);

        TransmissionPerks[0][2] = new Transmission(name: "Airborne III",
                                            description: "The contagious can be transmitted by airborne",
                                            perkLevel: 3,
                                            infected: 1f,
                                            transmission: ETransmission.Airborne,
                                            contagionRate: 0.60f);

        TransmissionPerks[0][3] = new Transmission(name: "Airborne IV",
                                            description: "The contagious can be transmitted by airborne",
                                            perkLevel: 4,
                                            infected: 1f,
                                            transmission: ETransmission.Airborne,
                                            contagionRate: 0.75f);
        // Food

        TransmissionPerks[1][0] = new Transmission(name: "Food I",
                                            description: "The contagious can be transmitted by Food",
                                            perkLevel: 1,
                                            infected: 1f,
                                            transmission: ETransmission.Food,
                                            contagionRate: 0.45f);

        TransmissionPerks[1][1] = new Transmission(name: "Food II",
                                            description: "The contagious can be transmitted by Food",
                                            perkLevel: 2,
                                            infected: 1f,
                                            transmission: ETransmission.Food,
                                            contagionRate: 0.45f);

        TransmissionPerks[1][2] = new Transmission(name: "Food III",
                                            description: "The contagious can be transmitted by Food",
                                            perkLevel: 3,
                                            infected: 1f,
                                            transmission: ETransmission.Food,
                                            contagionRate: 0.60f);

        TransmissionPerks[1][3] = new Transmission(name: "Food IV",
                                            description: "The contagious can be transmitted by Food",
                                            perkLevel: 4,
                                            infected: 1f,
                                            transmission: ETransmission.Food,
                                            contagionRate: 0.75f);
        // Water

        TransmissionPerks[2][0] = new Transmission(name: "Water I",
                                            description: "The contagious can be transmitted by Water",
                                            perkLevel: 1,
                                            infected: 1f,
                                            transmission: ETransmission.Water,
                                            contagionRate: 0.45f);

        TransmissionPerks[2][1] = new Transmission(name: "Water II",
                                            description: "The contagious can be transmitted by Water",
                                            perkLevel: 2,
                                            infected: 1f,
                                            transmission: ETransmission.Water,
                                            contagionRate: 0.45f);

        TransmissionPerks[2][2] = new Transmission(name: "Water III",
                                            description: "The contagious can be transmitted by Water",
                                            perkLevel: 3,
                                            infected: 1f,
                                            transmission: ETransmission.Water,
                                            contagionRate: 0.60f);

        TransmissionPerks[2][3] = new Transmission(name: "Water IV",
                                            description: "The contagious can be transmitted by Water",
                                            perkLevel: 4,
                                            infected: 1f,
                                            transmission: ETransmission.Water,
                                            contagionRate: 0.75f);
        // Zoonosis

        TransmissionPerks[3][0] = new Transmission(name: "Zoonosis I",
                                            description: "The contagious can be transmitted by Zoonosis",
                                            perkLevel: 1,
                                            infected: 1f,
                                            transmission: ETransmission.Zoonosis,
                                            contagionRate: 0.45f);

        TransmissionPerks[3][1] = new Transmission(name: "Zoonosis II",
                                            description: "The contagious can be transmitted by Zoonosis",
                                            perkLevel: 2,
                                            infected: 1f,
                                            transmission: ETransmission.Zoonosis,
                                            contagionRate: 0.45f);

        TransmissionPerks[3][2] = new Transmission(name: "Zoonosis III",
                                            description: "The contagious can be transmitted by Zoonosis",
                                            perkLevel: 3,
                                            infected: 1f,
                                            transmission: ETransmission.Zoonosis,
                                            contagionRate: 0.60f);

        TransmissionPerks[3][3] = new Transmission(name: "Zoonosis IV",
                                            description: "The contagious can be transmitted by Zoonosis",
                                            perkLevel: 4,
                                            infected: 1f,
                                            transmission: ETransmission.Zoonosis,
                                            contagionRate: 0.75f);

        cont = 0;
        foreach (Transmission[] t in TransmissionPerks)
        {
            foreach(Transmission tt in t)
            {
                if (cont == 0)
                {
                    tt.ImagePath = PerkPath + "airborne";
                }
                else if (cont == 1)
                {
                    tt.ImagePath = PerkPath + "food";
                }
                else if (cont == 2)
                {
                    tt.ImagePath = PerkPath + "water";
                }
                else if (cont == 3)
                {
                    tt.ImagePath = PerkPath + "zoonosis";
                }
            }
            cont++;
        }
    }
}