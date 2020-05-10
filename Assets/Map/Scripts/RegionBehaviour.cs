using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionBehaviour : MonoBehaviour
{
    public Color GroundColor;
    public Color WaterColor;
    public GameObject Select;

    public Region Region;

    private PopulationBehaviour populationBehaviour;

    void Start()
    {

        Color altitude = new Color(0, 0, 0, 1);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        populationBehaviour =
            GetComponentInChildren<PopulationBehaviour>();
        MarginHandlerBehaviour marginHandlerBehaviour =
            GetComponentInChildren<MarginHandlerBehaviour>();

        switch (Region.Type)
        {
            case RegionType.Ground:
                spriteRenderer.color =
                    Color.Lerp(altitude, GroundColor, 1 - Region.Altitude);

                populationBehaviour.SetPopulation(Region.PopulationDensity);
                break;
            case RegionType.Coast:
                spriteRenderer.color =
                    Color.Lerp(altitude, GroundColor, 1 - Region.Altitude);

                marginHandlerBehaviour.SetMargin(Region,
                    Color.Lerp(altitude, WaterColor, Region.Altitude));
                populationBehaviour.SetPopulation(Region.PopulationDensity);
                break;
            case RegionType.Water:
                spriteRenderer.color =
                    Color.Lerp(altitude, WaterColor, Region.Altitude);
                Destroy(populationBehaviour.gameObject);
                break;
        }

        GetComponentInChildren<DelimiterHandlerBehaviour>().SetDelimiter(Region);
    }

    public void ShowPopulation(bool show)
    {
        if (Region.Type != RegionType.Water)
            populationBehaviour.gameObject.SetActive(show);
    }

    void Update() { }

    private void OnMouseEnter()
    {
        Select.SetActive(true);
    }

    private void OnMouseExit()
    {
        Select.SetActive(false);
    }
}
