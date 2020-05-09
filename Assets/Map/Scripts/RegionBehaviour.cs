using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionBehaviour : MonoBehaviour
{
    [SerializeField]
    public Color GroundColor;
    public Color WaterColor;

    public Region Region;

    void Start()
    {
        Color altitude = new Color(0, 0, 0, 1);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (Region.Type)
        {
            case RegionType.Ground:
                spriteRenderer.color =
                    Color.Lerp(altitude, GroundColor, 1 - Region.Altitude);

                GetComponentInChildren<PopulationBehaviour>().
                    SetPopulation(Region.PopulationDensity);
                break;
            case RegionType.Coast:
                spriteRenderer.color =
                    Color.Lerp(altitude, GroundColor, 1 - Region.Altitude);
                //Color coastColor = Color.Lerp(GroundColor, WaterColor, 0.5f);
                //spriteRenderer.color =
                //    Color.Lerp(altitude, coastColor, 1 - Region.Altitude);

                GetComponentInChildren<MarginHandlerBehaviour>().SetMargin(
                    Region, Color.Lerp(altitude, WaterColor, Region.Altitude));
                GetComponentInChildren<PopulationBehaviour>().
                    SetPopulation(Region.PopulationDensity);
                break;
            case RegionType.Water:
                spriteRenderer.color =
                    Color.Lerp(altitude, WaterColor, Region.Altitude);
                Destroy(GetComponentInChildren<PopulationBehaviour>().gameObject);
                break;
        }

        GetComponentInChildren<DelimiterHandlerBehaviour>().SetDelimiter(Region);
    }

    void Update() { }
}
