using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationBehaviour : MonoBehaviour
{
    public Color PopulationColor;

    public void SetPopulation(float populationDensity)
    {
        GetComponent<SpriteRenderer>().color = new Color(
            PopulationColor.r,
            PopulationColor.g,
            PopulationColor.b, populationDensity);
        //transform.localScale = new Vector3(populationDensity, populationDensity, 0);
    }
}
