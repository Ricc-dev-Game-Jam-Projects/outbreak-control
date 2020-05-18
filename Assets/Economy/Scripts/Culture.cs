using System.Collections.Generic;
using UnityEngine;

public class Culture
{

    public string Description;
    public Dictionary<ETransmission, int> TransmissionWeakness;
    public Dictionary<ESystems, int> SystemWeakness;
    public float Warmness;
    public float StrangAnimalEating;
    public float SexualRelation;

    public Culture(string description)
    {
        Description = description;
        SystemWeakness = new Dictionary<ESystems, int>();
        TransmissionWeakness = new Dictionary<ETransmission, int>();
        Warmness = Random.Range(0, 2); //Randomizando a calorosidade da cultura
        StrangAnimalEating = Random.Range(0, 2); //Randomizando a quantidade de Animais Estranhos consumidos
        SexualRelation = Random.Range(0f, 2f); //quantidade de vezes que a cultura tem relacoes sexuais
    }

    public void GenerateCulture(Region region)
    {
        int pointsToUse = 24;
        // Para respiratório olhar altura
        float minLevel = MapBehaviour.instance.SeaLevel;
        float regionAlt = region.Altitude;
        float distanceFromSea = 0f;
        SystemWeakness.Add(ESystems.Respiratory, 
                    (int) Mathf.Clamp(Random.Range(0, regionAlt/4 + (distanceFromSea > 2 ? distanceFromSea/2 : 0)), 0, 4));
        pointsToUse -= SystemWeakness[ESystems.Respiratory]; // 4

        // Digestivo
        //Vou repetir um monte de Random aqui Riccardo, apaga nao por favor :)
        // Too late
        SystemWeakness.Add(ESystems.Immunologic, (int) Mathf.Clamp(SexualRelation, 0, 4));
        pointsToUse -= SystemWeakness[ESystems.Immunologic]; //8

        // Neurologic
        SystemWeakness.Add(ESystems.Neurologic, Random.Range(0, 4));
        pointsToUse -= SystemWeakness[ESystems.Neurologic]; //12

        // Digestive
        SystemWeakness.Add(ESystems.Digestive, Random.Range((int) StrangAnimalEating, 4));
        pointsToUse -= SystemWeakness[ESystems.Digestive]; //16

        TransmissionWeakness.Add(ETransmission.Airborne, Random.Range(0, 4));
        
        TransmissionWeakness.Add(ETransmission.Food, Random.Range(0, 4));

        TransmissionWeakness.Add(ETransmission.Water, Random.Range(0, 4));

        TransmissionWeakness.Add(ETransmission.Zoonosis, Random.Range(0, 4));
    }
}