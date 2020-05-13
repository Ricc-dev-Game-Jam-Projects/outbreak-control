using System.Collections.Generic;
using UnityEngine;

public  class Culture
{
    public string Description;
    public Dictionary<ETransmission, int> TransmissionWeakness;
    public Dictionary<ESystems, int> SystemWeakness;

    public Culture(string description)
    {
        SystemWeakness = new Dictionary<ESystems, int>();
        TransmissionWeakness = new Dictionary<ETransmission, int>();
        Description = description;
    }

    public void GenerateCulture(Region region)
    {
        // Para respiratório olhar altura
        float minLevel = MapBehaviour.instance.SeaLevel;
        float regionAlt = region.Altitude;
        float distanceFromSea = 0f;

        SystemWeakness.Add(
                    ESystems.Respiratory, 
                    (int) Mathf.Clamp(Random.Range(0, regionAlt/4 + (distanceFromSea <= 2 ? 3 - distanceFromSea : 0)), 0, 4));

        // Digestivo

        // Neurologic

        // Immunologic

        // Airborne

        // Food

        // Water

        // Zoonisis

    }
}

