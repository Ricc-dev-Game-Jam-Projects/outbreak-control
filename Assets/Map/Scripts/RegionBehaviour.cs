using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionBehaviour : MonoBehaviour
{
    public static RegionBehaviour RegionSelected;

    public Color GroundColor;
    public Color WaterColor;
    public GameObject Select;

    public Region Region;

    private PopulationBehaviour populationBehaviour;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        populationBehaviour = GetComponentInChildren<PopulationBehaviour>();
        MarginHandler marginHandler = GetComponentInChildren<MarginHandler>();
        RiverHandler riverHandler = GetComponentInChildren<RiverHandler>();

        switch (Region.Type)
        {
            case RegionType.Ground:
                spriteRenderer.color =
                    Color.Lerp(Color.black, GroundColor, 1 - Region.Altitude);

                populationBehaviour.SetPopulation(Region.city.RelPopulation);
                break;
            case RegionType.Coast:
                spriteRenderer.color =
                    Color.Lerp(Color.black, GroundColor, 1 - Region.Altitude);

                marginHandler.SetMargin(Region,
                    Color.Lerp(Color.black, WaterColor, Region.Altitude));
                populationBehaviour.SetPopulation(Region.city.RelPopulation);
                break;
            case RegionType.Water:
                spriteRenderer.color =
                    Color.Lerp(Color.black, WaterColor, Region.Altitude);
                Destroy(populationBehaviour.gameObject);
                break;
        }

        GetComponentInChildren<DelimiterHandler>().SetDelimiter(Region);

        if (Region.River.exists)
        {
            riverHandler.SetRiver(Region, WaterColor);
        }
    }

    public void UpdateRegion()
    {
        if(populationBehaviour != null)
            populationBehaviour.SetPopulation(Region.city.RelPopulation);
    }

    public void ShowPopulation(bool show)
    {
        if (Region.Type != RegionType.Water)
            populationBehaviour.gameObject.SetActive(show);
    }

    public void OnMouseUp()
    {
        RegionSelected = this;
    }

    private void OnMouseEnter()
    {
        Select.SetActive(true);
    }

    private void OnMouseExit()
    {
        Select.SetActive(false);
    }
}
