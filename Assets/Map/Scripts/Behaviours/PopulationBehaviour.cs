using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationBehaviour : MonoBehaviour
{
  public Color PopulationColor;

  public void SetPopulation(float Density, float symptomaticDensity)
  {
    GetComponent<SpriteRenderer>().color = new Color(
        PopulationColor.r,
        PopulationColor.g,
        PopulationColor.b + symptomaticDensity, 
        Density);
    //transform.localScale = new Vector3(populationDensity, populationDensity, 0);
  }
}
