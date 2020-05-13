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
        SystemWeakness = new Dictionary<ESystems, int>();
        TransmissionWeakness = new Dictionary<ETransmission, int>();
        Warmness = Random.Range(0, 2);
    }

    public void GenerateCulture(Region region)
    {
        int pointsToUse = 24;
        // Para respiratório olhar altura
        float minLevel = MapBehaviour.instance.SeaLevel;
        float regionAlt = region.Altitude;
        float distanceFromSea = 0f;

        SystemWeakness.Add(
                    ESystems.Respiratory, 
                    (int) Mathf.Clamp(Random.Range(0, regionAlt/4 + (distanceFromSea > 2 ? distanceFromSea/2 : 0)), 0, 4));

        pointsToUse -= SystemWeakness[ESystems.Respiratory];

        // Digestivo
        SystemWeakness.Add(
                    ESystems.Digestive,
                    2);

        // Neurologic

        // Immunologic

        // Airborne

        // Food

        // Water

        // Zoonisis

    }
}